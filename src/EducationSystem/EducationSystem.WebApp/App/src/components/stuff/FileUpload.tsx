import * as React from 'react'
import {Component} from 'react'
import {createStyles, Grid, IconButton, withStyles, WithStyles} from '@material-ui/core'
import PhotoCameraIcon from '@material-ui/icons/PhotoCamera'
import NoteAddIcon from '@material-ui/icons/NoteAdd'
import Clear from '@material-ui/icons/Clear'
import IFileService from '../../services/FileService'
import {inject} from '../../infrastructure/di/inject'
import FileModel from '../../models/FileModel'
import {If} from '../core'
import {FileType} from '../../common/enums'
import {Guid} from '../../helpers/guid'

const styles = () => createStyles({
  input: {
    display: 'none'
  }
})

interface IProps {
  onLoad: (fileModel?: FileModel) => any
  fileModel?: FileModel
  file?: File,
  type: FileType
}

type TProps = WithStyles<typeof styles> & IProps

interface IState {
  fileModel?: FileModel,
  extensions: Array<string>
}

class FileUpload extends Component<TProps, IState> {
  @inject
  private FileService?: IFileService

  constructor(props: TProps) {
    super(props)

    this.state = {
      fileModel: props.fileModel,
      extensions: []
    } as IState
  }

  async componentDidMount() {
    if (this.props.fileModel) return

    const {data, success} = await this.FileService!.getExtensions(this.props.type)

    if (success && data) {
      this.setState({extensions: data})
    }
  }

  componentWillReceiveProps(nextProps: Readonly<TProps>) {
    this.setState({
      fileModel: nextProps.fileModel
    })
  }

  handleAdd = async ({target: {files: [file]}}: any) => {
    let form = new FormData()
    form.append('file', file)

    const {data, success} = await this.FileService!.add(form, this.props.type)

    if (success && data) {
      this.setState({fileModel: data}, () => this.props.onLoad((data)))
    }
  }

  handleDelete = async () => {
    const {fileModel} = this.state

    if (fileModel && fileModel.Id) {
      await this.FileService!.delete(fileModel.Id, this.props.type)
      this.setState({}, this.props.onLoad)
    }
  }

  render() {
    const {classes} = this.props

    const id = Guid.create()

    return <Grid item xs={12} container>
      <If condition={this.state.extensions.length > 0 || this.props.fileModel} orElse={<></>}>
        <If condition={this.state.fileModel} orElse={
          <Grid item xs={6}>
            <input
              accept={this.state.extensions.join(', ')}
              className={classes.input}
              id={id}
              onChange={this.handleAdd}
              type="file"
            />
            <label htmlFor={id}>
              <IconButton component="span">
                {this.props.type == FileType.Image && <PhotoCameraIcon/> || <NoteAddIcon/>}
              </IconButton>
            </label>
          </Grid>
        }>
          <Grid item xs={6}>
            <IconButton component="span" onClick={this.handleDelete}>
              <Clear/>
            </IconButton>
          </Grid>
        </If>
      </If>
    </Grid>
  }
}

export default withStyles(styles)(FileUpload) as any