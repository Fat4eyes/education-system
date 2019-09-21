import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import {FormControl, Grid, InputLabel, Typography, withStyles, WithStyles} from '@material-ui/core'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import MaterialStyles from './MaterialStyles'
import IFileService from '../../../services/FileService'
import {inject} from '../../../infrastructure/di/inject'
import Material, {IMaterialAnchor} from '../../../models/Material'
import DocumentFile from '../../../models/DocumentFile'
import MaterialEditor from '../../MaterialEditor/MaterialEditor'
import IMaterialService from '../../../services/MaterialService'
import Block from '../../Blocks/Block'
import INotificationService from '../../../services/NotificationService'
import {MtBlock} from '../../stuff/Margin'
import withWidth, {WithWidth} from '@material-ui/core/withWidth/withWidth'
import {PopoverProps} from '@material-ui/core/Popover'
import Input from '../../stuff/Input'
import Button from '../../stuff/Button'
import FileHandler from './FileHandler'

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
  IsLoading: boolean,
  FileSelectAnchor: PopoverProps['anchorEl'],
  Documents: Array<DocumentFile>,
  SelectedDocumentsIds: Array<number>,
}

class MaterialHandling extends Component<TProps, IState> {
  @inject private FileService?: IFileService
  @inject private MaterialService?: IMaterialService
  @inject private NotificationService?: INotificationService

  constructor(props: TProps) {
    super(props)

    this.state = {
      Model: new Material(),
      IsLoading: false,
      FileSelectAnchor: null,
      Documents: [],
      SelectedDocumentsIds: []
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
  
  handleAddFile = (file: DocumentFile) => {
    return this.setState(state => ({
      Model: {
        ...state.Model,
        Files: [...state.Model.Files, file]
      }
    }))
  }

  handleRemoveFile = (file: DocumentFile) => {
    return this.setState(state => ({
      Model: {
        ...state.Model,
        Files: state.Model.Files.filter(f => f.Id !== file.Id)
      }
    }))
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

    return <Grid container justify='center'>
      <Grid item xs={12}>
        <Block partial>
          <Grid item xs={12} container className={classes.header} zeroMinWidth wrap='nowrap'>
            <Typography variant='subtitle1' className={classes.headerText} noWrap>
              {this.state.Model.Id ? 'Редактирование' : 'Создание'} материала
            </Typography>
          </Grid>
          <MtBlock value={3}/>
          <Grid item xs={12} container>
            <FormControl fullWidth>
              <InputLabel shrink htmlFor='Text'>
                Название:
              </InputLabel>
              <Input
                value={this.state.Model.Name}
                name='Name'
                onChange={this.handleModel}
                fullWidth
              />
            </FormControl>
          </Grid>
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
            <FileHandler
              handleAddFile={this.handleAddFile}
              handleRemoveFile={this.handleRemoveFile}
              fileService={this.FileService!}
              files={this.state.Model.Files}
            />
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