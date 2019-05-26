import * as React from 'react'
import {ChangeEvent, ChangeEventHandler, Component} from 'react'
import {Button, Chip, Collapse, Grid, TextField, Typography, withStyles, WithStyles} from '@material-ui/core'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import MaterialStyles from './MaterialStyles'
import IFileService from '../../../services/FileService'
import {inject} from '../../../infrastructure/di/inject'
import Material, {IMaterialAnchor} from '../../../models/Material'
import DocumentFile from '../../../models/DocumentFile'
import FileUpload, {FileInput} from '../../stuff/FileUpload'
import {FileType} from '../../../common/enums'
import MaterialEditor from '../../MaterialEditor/MaterialEditor'
import IMaterialService from '../../../services/MaterialService'
import Block from '../../Blocks/Block'
import INotificationService from '../../../services/NotificationService'
import {MrBlock, MtBlock} from '../../stuff/Margin'
import NoteAddIcon from '@material-ui/icons/NoteAdd'
import {Guid} from '../../../helpers/guid'
import withWidth, {isWidthDown, WithWidth} from '@material-ui/core/withWidth/withWidth'
import {PopoverProps} from '@material-ui/core/Popover'
import FileSelect from './FileSelect'

interface IProps {
  match?: {
    params: {
      id?: number,
    }
  },
  onMaterialSave: (material?: Material) => void
}

type TProps = WithStyles<typeof MaterialStyles> & InjectedNotistackProps & IProps & WithWidth

interface IState {
  Model: Material,
  IsMaterialEditorOpen: boolean,
  IsLoading: boolean,
  FileSelectAnchor: PopoverProps['anchorEl'],
  Documents: Array<DocumentFile>
}

class MaterialHandling extends Component<TProps, IState> {
  @inject private FileService?: IFileService
  @inject private MaterialService?: IMaterialService
  @inject private NotificationService?: INotificationService

  constructor(props: TProps) {
    super(props)

    this.state = {
      Model: new Material(),
      IsMaterialEditorOpen: false,
      IsLoading: false,
      FileSelectAnchor: null,
      Documents: []
    }
  }

  componentDidMount() {
    if (!this.props.match || !this.props.match.params.id) return

    this.setState({IsLoading: true}, async () => {
      const {data, success} = await this.MaterialService!.get(this.props.match!.params.id!)

      if (success && data) {
        this.setState({
          Model: {
            Files: [],
            ...data
          },
          IsLoading: false
        })
      }
    })
  }

  handleModel = ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any) => {
    this.setState(state => ({
      Model: {
        ...state.Model,
        [name]: value
      }
    }))
  }

  handleMaterialEditorVisible = () => {
    this.setState(state => ({
      IsMaterialEditorOpen: !state.IsMaterialEditorOpen
    }))
  }

  handleFile = (index?: number) => (file?: DocumentFile) => {
    if (file) {
      return this.setState(state => ({
        Model: {
          ...state.Model,
          Files: [...state.Model.Files, file]
        }
      }))
    }

    if (index !== undefined) {
      this.state.Model.Files.splice(index, 1)
      return this.forceUpdate()
    }
  }

  handleSubmit = async () => {
    let material = this.state.Model

    let result = false
    if (this.state.Model.Id) {
      result = await this.MaterialService!.update(material)
      result && this.NotificationService!.showSuccess(`Материал успешно обновлен`)
    } else {
      const {data, success} = await this.MaterialService!.add(material)
      if (data && success) {
        result = success
        material.Id = data
        this.NotificationService!.showSuccess(`Материал успешно добавлен`)
      }
    }

    result && this.props.onMaterialSave && this.props.onMaterialSave(material)
  }
  
  handleFileSelect = async (anchorEl: PopoverProps['anchorEl']) => {
    if (!anchorEl) return this.setState({FileSelectAnchor: anchorEl})
    
    if (!this.state.Documents.length) {
      const {data, success} = await this.FileService!.getAll(FileType.Document, {All: true})
      if (data && success) {
        return this.setState({
          Documents: data.Items,
          FileSelectAnchor: anchorEl
        })
      } 
    } else {
      return this.setState({FileSelectAnchor: anchorEl})
    }
  }

  render(): React.ReactNode {
    let {classes, width} = this.props
    let isSmallScreen = isWidthDown('md', width);

    return <Grid container justify='center' spacing={16}>
      <Grid item xs={12} md={10} lg={8}>
        <Block partial>
          <Grid item xs={12} container className={classes.header} zeroMinWidth wrap='nowrap'>
            <Typography variant='subtitle1' className={classes.headerText} noWrap>
              {this.state.Model.Id ? 'Редактирование' : 'Создание'} материала
            </Typography>
          </Grid>
          <MtBlock value={2}/>
          <Grid item xs={12} container>
            <TextField fullWidth
                       label='Название'
                       required
                       name='Name'
                       value={this.state.Model.Name}
                       onChange={this.handleModel}
            />
          </Grid>
          <MtBlock value={2}/>
          <Grid item xs={12} container>
            <Button onClick={this.handleMaterialEditorVisible} className={classes.openEditorButton}>
              <Typography noWrap variant='subtitle1'>
                {this.state.IsMaterialEditorOpen ? 'Закрыть ' : 'Открыть '} редактор
              </Typography>
            </Button>
          </Grid>
          <Collapse timeout={500} in={this.state.IsMaterialEditorOpen}>
            <MtBlock value={2}/>
            <Grid item xs={12} container>
              {!this.state.IsLoading &&
              <MaterialEditor
                export={(html: string) => this.handleModel({target: {name: 'Template', value: html}})}
                import={this.state.Model.Template}
                setAnchor={(anchor: IMaterialAnchor) => this.setState(state => ({
                  Model: {
                    ...state.Model,
                    Anchors: [...state.Model.Anchors, anchor]
                  }
                }))}
                removeAnchor={(token: string) => {
                  return this.setState(state => ({
                    Model: {
                      ...state.Model,
                      Anchors: [...state.Model.Anchors.filter(a => a.Token !== token)]
                    }
                  }))
                }}
                materialAnchors={this.state.Model.Anchors}
              />
              }
            </Grid>
            <MtBlock value={2}/>
            <Grid item xs={12} container>
              <Button onClick={this.handleMaterialEditorVisible} className={classes.openEditorButton}>
                <Typography noWrap variant='subtitle1'>
                  {this.state.IsMaterialEditorOpen ? 'Закрыть ' : 'Открыть '} редактор
                </Typography>
              </Button>
            </Grid>
          </Collapse>
          <MtBlock value={2}/>
          <Grid item xs={12} container>
            <Grid item xs container alignItems='center'>
              <Grid item xs={12} md>
                <FileUpload onLoad={this.handleFile()} type={FileType.Document}>
                  {(handleAdd: ChangeEventHandler, extensions: string[]) => {
                    if (!extensions) return <></>
                    const id = Guid.create()
                    return <>
                      <FileInput extensions={extensions} id={id} onChange={handleAdd}/>
                      <label htmlFor={id} style={{width: '100%'}}>
                        <Button component='span' className={classes.openEditorButton}>
                          <Typography noWrap variant='subtitle1'>
                            Загрузить документ
                          </Typography>
                        </Button>
                      </label>
                    </>
                  }}
                </FileUpload>
              </Grid>
              {isSmallScreen ? <MtBlock/> : <MrBlock/>}
              <Grid item xs={12} md>
                <Button component='span' 
                        className={classes.openEditorButton} 
                        onClick={e => this.handleFileSelect(e.currentTarget)}
                >
                  <Typography noWrap variant='subtitle1'>
                    Прикрепить документ
                  </Typography>
                </Button>
                <FileSelect
                  documents={this.state.Documents}
                  onClose={(file?: DocumentFile) => {
                    this.handleFileSelect(null)
                    this.handleFile()(file)
                  }}
                  anchorEl={this.state.FileSelectAnchor}
                  isOpen={this.state.FileSelectAnchor !== null}
                />
              </Grid>
            </Grid>
            <MtBlock/>
            <Grid item xs={12} container>
              {this.state.Model.Files.map((file: DocumentFile, index: number) =>
                <Grid item key={file.Id || index}>
                  <FileUpload onLoad={this.handleFile(index)} fileModel={file} type={FileType.Document}>
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
                </Grid>
              )}
            </Grid>
            <MtBlock value={2}/>
            <Grid item xs={12} container>
              <Button onClick={this.handleSubmit} variant='outlined'>
                <Typography noWrap variant='subtitle1'>
                  Сохранить
                </Typography>
              </Button>
            </Grid>
          </Grid>
        </Block>
      </Grid>
    </Grid>
  }
}

export default withWidth()(withSnackbar(withStyles(MaterialStyles)(MaterialHandling))) as any