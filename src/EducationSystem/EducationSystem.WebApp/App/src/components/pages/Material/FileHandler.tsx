import * as React from 'react'
import {ChangeEventHandler, FunctionComponent, useEffect, useState} from 'react'
import {Chip, createStyles, Grid, Theme, Typography, withStyles, WithStyles} from '@material-ui/core'
import FileUpload, {FileInput} from '../../stuff/FileUpload'
import {FileType} from '../../../common/enums'
import Button from '../../stuff/Button'
import {MrBlock, MtBlock} from '../../stuff/Margin'
import FileSelect from './FileSelect'
import DocumentFile from '../../../models/DocumentFile'
import {isWidthDown, WithWidth} from '@material-ui/core/withWidth'
import withWidth from '@material-ui/core/withWidth/withWidth'
import {important, onHover} from '../../stuff/CommonStyles'
import {PopoverProps} from '@material-ui/core/Popover'
import IFileService from '../../../services/FileService'
import NoteAddIcon from '@material-ui/icons/NoteAdd'
import FileModel from '../../../models/FileModel'

const styles = (theme: Theme) => createStyles({
  root: {},
  button: {
    padding: 0 + important,
    width: '100%',
    backgroundColor: theme.palette.grey['300'],
    ...onHover({
      backgroundColor: theme.palette.grey['400']
    })
  },
  chip: {
    margin: '6px 12px'
  }
})

interface IProps {
  handleAddFile: (file: DocumentFile) => void
  handleRemoveFile: (file: DocumentFile) => void
  fileService: IFileService
  files: DocumentFile[]
}

type TProps = IProps & WithStyles<typeof styles> & WithWidth

interface IState {
  attachedFilesIds: number[]
  anchor: PopoverProps['anchorEl']
}

const FileHandler: FunctionComponent<TProps> = ({classes, width, ...props}: TProps) => {
  const [state, setState] = useState<IState>({
    attachedFilesIds: [],
    anchor: null
  })
  const setAnchor = (anchor: PopoverProps['anchorEl']) => setState({...state, anchor})
  const removeAttachedFileId = (id: number) => setState({
    ...state,
    attachedFilesIds: state.attachedFilesIds.filter(i => i !== id)
  })

  const [documents, setDocuments] = useState<DocumentFile[]>([])
  const filterDocuments = (files: FileModel[]) => files.filter(ff => !props.files.find(f => f.Id === ff.Id))

  useEffect(() => {
    if (state.anchor && !documents.length) {
      props.fileService
        .getAll(FileType.Document, {All: true})
        .then(({data}) => data && setDocuments(filterDocuments(data.Items)))
    }
  }, [state.anchor])

  useEffect(() => setDocuments(filterDocuments(documents)), [props.files])

  return <>
    <Grid item xs container alignItems='center'>
      <Grid item xs={12} md>
        <FileUpload onLoad={props.handleAddFile} type={FileType.Document}>
          {(handleAdd: ChangeEventHandler, extensions: string[], id: string) => {
            return <>
              <FileInput extensions={extensions} id={id} onChange={handleAdd}/>
              <label htmlFor={id} style={{width: '100%'}}>
                <Button className={classes.button} component='span'>
                  <Typography noWrap variant='subtitle1'>Загрузить документ</Typography>
                </Button>
              </label>
            </>
          }}
        </FileUpload>
      </Grid>
      {isWidthDown('md', width) ? <MtBlock/> : <MrBlock/>}
      <Grid item xs={12} md>
        <Button className={classes.button} onClick={e => setAnchor(e.currentTarget)}>
          <Typography noWrap variant='subtitle1'>Прикрепить документ</Typography>
        </Button>
        <FileSelect documents={documents} anchorEl={state.anchor} isOpen={state.anchor !== null && !!documents.length}
                    onClose={(file?: DocumentFile) => {
                      setState({
                        anchor: null,
                        attachedFilesIds: file ? [...state.attachedFilesIds, file.Id!] : state.attachedFilesIds
                      })
                      file && props.handleAddFile(file)
                    }}
        />
      </Grid>
    </Grid>
    <MtBlock/>
    <Grid item xs={12} container>
      {props.files.map((file: DocumentFile, index: number) =>
        <Grid item key={file.Id || index}>
          {state.attachedFilesIds.includes(file.Id!)
            ? <Chip
              className={classes.chip}
              key={file.Id || index}
              icon={<NoteAddIcon/>}
              label={file.Name}
              onDelete={() => {
                props.handleRemoveFile(file)
                removeAttachedFileId(file.Id!)
              }}
              variant='outlined'
            />
            : <FileUpload onLoad={() => props.handleRemoveFile(file)} fileModel={file} type={FileType.Document}>
              {(deleteHandler: () => void) =>
                <Chip
                  className={classes.chip}
                  key={file.Id || index}
                  icon={<NoteAddIcon/>}
                  label={file.Name}
                  onDelete={deleteHandler}
                  variant='outlined'
                />
              }
            </FileUpload>
          }
        </Grid>
      )}
    </Grid>
  </>
}

export default withWidth()(withStyles(styles)(FileHandler)) as FunctionComponent<IProps>