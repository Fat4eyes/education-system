import * as React from 'react'
import {Component} from 'react'
import {createStyles, Grid, IconButton, withStyles, WithStyles} from '@material-ui/core'
import PhotoCamera from '@material-ui/icons/PhotoCamera'
import Clear from '@material-ui/icons/Clear'
import {ProtectedFetch} from '../../helpers'
import IFileService from '../../services/abstractions/IFileService'
import {inject} from '../../infrastructure/di/inject'

const styles = () => createStyles({
  input: {
    display: 'none'
  }
})

interface IProps {
  onLoad: (file?: File, id?: number) => any
}

type TProps = WithStyles<typeof styles> & IProps

interface IState {
  fileResult: any
}

class FileUpload extends Component<TProps, IState> {
  @inject
  private FileService?: IFileService

  constructor(props: TProps) {
    super(props)

    this.state = {
      fileResult: {
        name: '',
        id: undefined
      }
    } as IState
  }

  handleAdd = async ({target}: any) => {
    let form = new FormData()
    let file = target.files[0]
    form.append('file', file)
    let result = await this.FileService!.add(form)

    this.setState({fileResult: result}, () => this.props.onLoad(file, result.Id))
  }

  handleDelete = async () => {
    await this.FileService!.delete(this.state.fileResult.Id)
    this.setState({fileResult: null}, () => this.props.onLoad())
  }

  render() {
    const {classes} = this.props

    return (
      <Grid item xs={12} container>
        <Grid item xs={6}>
          <input
            accept="image/*"
            className={classes.input}
            id="icon-button-photo"
            onChange={this.handleAdd}
            type="file"
          />
          <label htmlFor="icon-button-photo">
            <IconButton color="primary" component="span">
              <PhotoCamera/>
            </IconButton>
          </label>
        </Grid>
        {this.state.fileResult &&
        <Grid item xs={6}>
          <IconButton color="primary" component="span" onClick={this.handleDelete}>
            <Clear/>
          </IconButton>
        </Grid>
        }
      </Grid>
    )
  }
}

export default withStyles(styles)(FileUpload) as any