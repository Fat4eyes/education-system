import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import {Button, Collapse, Grid, TextField, Typography, withStyles, WithStyles} from '@material-ui/core'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import MaterialStyles from './MaterialStyles'
import IFileService from '../../../services/FileService'
import {inject} from '../../../infrastructure/di/inject'
import Material from '../../../models/Material'
import DocumentFile from '../../../models/DocumentFile'
import FileUpload from '../../stuff/FileUpload'
import {FileType} from '../../../common/enums'
import MaterialEditor from '../../MaterialEditor/MaterialEditor'
import IMaterialService from '../../../services/MaterialService'
import Block from '../../Blocks/Block'
import INotificationService from '../../../services/NotificationService'

interface IProps {
  match?: {
    params: {
      id?: number,
    }
  },
  onMaterialSave: (material?: Material) => void
}

type TProps = WithStyles<typeof MaterialStyles> & InjectedNotistackProps & IProps

interface IState {
  Model: Material,
  IsMaterialEditorOpen: boolean,
  IsLoading: boolean
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
      IsLoading: false
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

  render(): React.ReactNode {
    let {classes} = this.props

    return <Grid container justify='center' spacing={16}>
      <Grid item xs={12} md={10} lg={8}>
        <Block>
          <Grid item xs={12} container spacing={16}>
            <TextField fullWidth
                       label='Название'
                       required
                       name='Name'
                       value={this.state.Model.Name}
                       onChange={this.handleModel}
            />
          </Grid>
          <Grid item xs={12} container spacing={16}>
            <Button onClick={this.handleMaterialEditorVisible}>
              <Typography noWrap variant='subtitle1'>
                {this.state.IsMaterialEditorOpen ? 'Закрыть ' : 'Открыть '} редактор
              </Typography>
            </Button>
          </Grid>
          <Collapse timeout={500} in={this.state.IsMaterialEditorOpen}>
            <Grid item xs={12} container spacing={16}>
              {!this.state.IsLoading &&
              <MaterialEditor
                export={(html: string) => this.handleModel({target: {name: 'Template', value: html}})}
                import={this.state.Model.Template}
              />
              }
            </Grid>
          </Collapse>
          <Grid item xs={12} container spacing={16}>
            {this.state.Model.Files.map((file: DocumentFile, index: number) =>
              <Grid item xs={12} key={file.Id!} container alignItems='center'>
                <Grid item>
                  <FileUpload onLoad={this.handleFile(index)} fileModel={file} type={FileType.Document}/>
                </Grid>
                <Grid item xs container wrap='nowrap' zeroMinWidth>
                  <Typography noWrap variant='subtitle1'>
                    {file.Name}
                  </Typography>
                </Grid>
              </Grid>
            )}
            <Grid item xs container alignItems='center'>
              <Grid item>
                <FileUpload onLoad={this.handleFile()} type={FileType.Document}/>
              </Grid>
              <Grid item xs container wrap='nowrap' zeroMinWidth>
                <Typography noWrap variant='subtitle1'>
                  Прикрепить документ
                </Typography>
              </Grid>
            </Grid>
            <Grid item xs={12} container spacing={16}>
              <Button onClick={this.handleSubmit}>
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

export default withSnackbar(withStyles(MaterialStyles)(MaterialHandling) as any)