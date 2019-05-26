import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import {
  Button, Collapse,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  TextField, Typography,
  withStyles,
  WithStyles
} from '@material-ui/core'
import QuestionHandlingStyle from './QuestionHandlingStyle'
import Question from '../../../models/Question'
import TotalTimeInput from '../HandleTest/TotalTimeInput'
import {FileType, QuestionComplexityType, QuestionType} from '../../../common/enums'
import {AnswersHandling} from './AnswerHandling'
import Answer from '../../../models/Answer'
import Program from '../../../models/Program'
import IQuestionService from '../../../services/QuestionService'
import {inject} from '../../../infrastructure/di/inject'
import FileUpload from '../../stuff/FileUpload'
import IFileService from '../../../services/FileService'
import FileModel from '../../../models/FileModel'
import MaterialSelect from '../../Table/MaterialSelect'
import Material, {IMaterialAnchor} from '../../../models/Material'
import Block from '../../Blocks/Block'
import INotificationService from '../../../services/NotificationService'
import {MtBlock} from '../../stuff/Margin'

interface IProps {
  match: {
    params: {
      themeId: number,
      id: number,
    }
  }
}

type TProps = WithStyles<typeof QuestionHandlingStyle> & IProps

interface IState {
  Model: Question,
  IsMaterialSelectOpen: boolean,
  SelectedAnchors: Array<number>
}

class QuestionHandling extends Component<TProps, IState> {
  @inject private QuestionService?: IQuestionService
  @inject private FileService?: IFileService
  @inject private NotificationService?: INotificationService

  constructor(props: TProps) {
    super(props)

    this.state = {
      Model: new Question(this.props.match.params.themeId),
      IsMaterialSelectOpen: false
    } as IState
  }

  async componentDidMount() {
    let {id} = this.props.match.params

    if (!id) return

    const {data, success} = await this.QuestionService!.get(id)

    if (success && data) {
      this.setState({
        Model: {
          Answers: [],
          MaterialAnchors: [],
          ...data
        }
      })
    }
  }

  handleModel = ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any) => {
    this.setState(state => ({
      Model: {
        ...state.Model,
        [name]: value
      }
    }))
  }

  handleType = ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any) => {
    this.state.Model.Answers.forEach((answer: Answer) => {
      switch (value) {
        case QuestionType.ClosedManyAnswers:
        case QuestionType.ClosedOneAnswer:
          answer.IsRight = false
          break
        case QuestionType.OpenedOneString:
          answer.IsRight = true
          break
        default:
          answer.IsRight = undefined
          break
      }
    })
    this.setState(state => ({
      Model: {
        ...state.Model,
        [name]: value
      }
    }))
  }

  handleAnswers = (answers: Array<Answer>) => this.handleModel({target: {name: 'Answers', value: answers}})
  handleProgram = (program: Program) => this.handleModel({target: {name: 'Program', value: program}})

  handleSubmit = async () => {
    if (this.props.match.params.id) {
      if (await this.QuestionService!.update(this.state.Model)) {
        this.NotificationService!.showSuccess('Вопрос успешно обновлен')
      }
    } else {
      const {data, success} = await this.QuestionService!.add(this.state.Model)

      if (success && data) {
        this.NotificationService!.showSuccess('Вопрос успешно добавлен')
      }
    }
  }

  handleImageLoad = (fileModel?: FileModel) =>
    this.setState(state => ({
      Model: {
        ...state.Model,
        Image: fileModel
      }
    }))

  async componentWillUnmount() {
    const {Image} = this.state.Model
    Image && Image.Id && !this.state.Model.Id && await this.FileService!.delete(Image.Id, FileType.Image)
  }

  handleMaterialSelect = (material?: Material) => {
    this.setState(state => ({
      Model: {
        ...state.Model,
        Material: material,
        MaterialAnchors: !material ? [] : state.Model.MaterialAnchors
      }
    }))
  }
  
  handleMaterialSelectOpen = () => this.setState(state => ({IsMaterialSelectOpen: !state.IsMaterialSelectOpen}))

  handleSelectAnchors = ({target: {value}}: ChangeEvent<HTMLInputElement> | any) => {
    this.setState(state => ({
      Model: {
        ...state.Model,
        MaterialAnchors: state.Model.Material!
          .Anchors.filter((anchor: IMaterialAnchor) => (value as Array<number>).includes(anchor.Id))
      },
      SelectedAnchors: [...value]
    }))
  }
  
  render(): React.ReactNode {
    let {classes} = this.props

    let imageSrc = ((): string | false => {
      const {Image} = this.state.Model

      if (Image && Image.Path)
        return `${window.location.origin}/${Image.Path}`

      return false
    })()

    let HandledInputs = () => <>
      <Grid item xs={12} sm={6} md={4} lg={2}>
        <TotalTimeInput name='Time' label='Длительность теста'
                        value={this.state.Model.Time} onChange={this.handleModel}
                        required type='duration'
                        validators={{max: {value: 3600, message: 'Тест не может быть больше 60 минут'}}}
                        mask={[/[0-9]/, /[0-9]/, ':', /[0-5]/, /[0-9]/]}
        />
      </Grid>
      <Grid item xs={12} sm={6} md={4} lg={7}>
        <FormControl fullWidth margin='normal' required>
          <InputLabel htmlFor='Type'>Тип</InputLabel>
          <Select name='Type' value={this.state.Model.Type} onChange={this.handleType}>
            <MenuItem value={QuestionType.ClosedOneAnswer}>Закрытый с одним правильным ответом</MenuItem>
            <MenuItem value={QuestionType.ClosedManyAnswers}>Закрытый с несколькими правильными ответами</MenuItem>
            <MenuItem value={QuestionType.OpenedOneString}>Открытый однострочный</MenuItem>
            <MenuItem value={QuestionType.OpenedManyStrings}>Открытый многострочный</MenuItem>
            <MenuItem value={QuestionType.WithProgram}>Программный код</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      <Grid item xs={12} sm={6} md={4} lg={3}>
        <FormControl fullWidth margin='normal' required>
          <InputLabel htmlFor='Complexity'>Сложность</InputLabel>
          <Select name='Complexity' value={this.state.Model.Complexity} onChange={this.handleModel}>
            <MenuItem value={QuestionComplexityType.Low}>Лёгкий</MenuItem>
            <MenuItem value={QuestionComplexityType.Medium}>Средний</MenuItem>
            <MenuItem value={QuestionComplexityType.High}>Сложный</MenuItem>
          </Select>
        </FormControl>
      </Grid>
    </>

    return <Grid container justify='center'>
      <Grid item xs={12} md={10} lg={8}>
        <Block partial>
          <Grid item xs={12} container className={classes.header} zeroMinWidth wrap='nowrap'>
            <Typography variant='subtitle1' className={classes.headerText} noWrap>
              {this.state.Model.Id ? 'Редактирование' : 'Создание'} вопроса
            </Typography>
          </Grid>
          <MtBlock value={2}/>
          <Grid item xs={12} container spacing={16}>
            <Grid item xs={12}>
              <TextField name='Text' label='Текст вопроса' required multiline fullWidth rows={5}
                         value={this.state.Model.Text} onChange={this.handleModel}/>
            </Grid>
          </Grid>
          <MtBlock value={2}/>
          <Grid item xs={12} container spacing={16}>
            <HandledInputs/>
          </Grid>
          <Grid item xs={12} container spacing={16}>
            <Grid item>
              <FileUpload onLoad={this.handleImageLoad} fileModel={this.state.Model.Image} type={FileType.Image}/>
            </Grid>
            {imageSrc && <Grid item xs={10}><img className={classes.image} src={imageSrc} alt={'Foto'}/></Grid>}
          </Grid>
          <Grid item xs={12} container spacing={16}>
            <AnswersHandling type={this.state.Model.Type}
                             incomingAnswers={this.state.Model.Answers}
                             handleAnswers={this.handleAnswers}
                             handleProgram={this.handleProgram}
            />
          </Grid>
          <MtBlock value={2}/>
          <Grid item xs={12} container>
            <Grid item xs={12}>
              <Button onClick={this.handleMaterialSelectOpen} className={classes.openButton}>
                <Typography noWrap variant='subtitle1'>
                  {this.state.IsMaterialSelectOpen ? 'Закрыть ' : 'Открыть '} выбор материала
                </Typography>
              </Button>
            </Grid>
            <MtBlock value={2}/>
            <Grid item xs={12}>
              <Collapse timeout={500} in={this.state.IsMaterialSelectOpen}>
                <MaterialSelect 
                  onSelectMaterial={this.handleMaterialSelect} 
                  selectedMaterial={this.state.Model.Material}
                  handleSelectAnchors={this.handleSelectAnchors}
                  selectedAnchors={this.state.Model.MaterialAnchors}
                />
              </Collapse>
            </Grid>
          </Grid>
          <MtBlock value={2}/>
          <Grid item xs={12} container>
            <Button onClick={this.handleSubmit} variant='outlined'>
              {this.state.Model.Id ? 'Обновить вопрос' : 'Добавить вопрос'}
            </Button>
          </Grid>
        </Block>
      </Grid>
    </Grid>
  }
}

export default withStyles(QuestionHandlingStyle)(QuestionHandling) as any