import * as React from 'react'
import {FunctionComponent, useState} from 'react'
import {createStyles, Popover, TextField, Theme, withStyles, WithStyles} from '@material-ui/core'
import Grid from '@material-ui/core/Grid'
import {EditorState} from 'draft-js'
import {StaticToolbar} from './StaticToolbar/StaticToolbar'
import FileUpload from '../stuff/FileUpload'
import {FileType} from '../../common/enums'
import IconButton from '@material-ui/core/IconButton'
import {findAnchor} from './MaterialEditorTools'
import FileModel from '../../models/FileModel'
import AddIcon from '@material-ui/icons/Add'
import ClearIcon from '@material-ui/icons/Clear'
import AnchorIcon from '@material-ui/icons/StarBorder'
import StarIcon from '@material-ui/icons/Star'

const styles = (theme: Theme) => createStyles({
  menu: {
    padding: theme.spacing.unit * 3
  }
})

interface IProps {
  editorState: EditorState,
  onLoadImage: (file: FileModel) => void
  onSetAnchor: (name: string) => boolean
  onRemove: (token: string) => void
}

type TProps = IProps & WithStyles<typeof styles>

const EditorToolbar: FunctionComponent<TProps> = (
  {classes, editorState, ...props}: TProps
) => {
  const [anchorName, setAnchorName] = useState<string>('')
  const [anchorEl, setAnchorEl] = useState<any>(null)
  const selectedBlockAnchorKey = editorState.getSelection().getAnchorKey()
  const anchorElement = document.getElementById(`anchor-${selectedBlockAnchorKey}`)

  return <Grid container justify='center' alignItems='center'>
    <Grid item>
      <StaticToolbar/>
    </Grid>
    <Grid item>
      <FileUpload type={FileType.Image} onLoad={props.onLoadImage}/>
    </Grid>
    <Grid item xs/>
    <Grid item>
      {!anchorElement
        ? <IconButton onClick={({currentTarget}) => setAnchorEl(currentTarget)}>
          <AnchorIcon/>
        </IconButton>
        : <IconButton onClick={() => {
          let anchoredElement = findAnchor(selectedBlockAnchorKey)
          if (anchoredElement) {
            anchoredElement.className = ''
          }
          anchorElement.remove()
          props.onRemove(selectedBlockAnchorKey)
        }}>
          <StarIcon/>
        </IconButton>
      }
    </Grid>
    <Popover
      open={!!anchorEl}
      anchorEl={anchorEl}
      onClose={() => setAnchorEl(null)}
      anchorOrigin={{vertical: 'bottom', horizontal: 'center'}}
      transformOrigin={{vertical: 'top', horizontal: 'center'}}
    >
      <Grid item container className={classes.menu}>
        <Grid item xs>
          <TextField
            autoFocus
            label='Якорь'
            placeholder='Введите название якоря'
            value={anchorName}
            onChange={({target: {value}}) => setAnchorName(value)}
            fullWidth
            margin='none'
          />
        </Grid>
        <Grid item>
          <IconButton onClick={() => {
            if (props.onSetAnchor(anchorName)) {
              setAnchorEl(null)
              setAnchorName('')
            }
          }}>
            <AddIcon/>
          </IconButton>
        </Grid>
        <Grid item>
          <IconButton onClick={() => setAnchorEl(null)}>
            <ClearIcon/>
          </IconButton>
        </Grid>
      </Grid>
    </Popover>
  </Grid>
}

export default withStyles(styles)(EditorToolbar) as FunctionComponent<IProps>