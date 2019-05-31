import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import {FormControl, Grid, InputLabel, MenuItem, Select, Typography, withStyles, WithStyles} from '@material-ui/core'
import QuestionHandlingStyle from './QuestionHandlingStyle'
import Question from '../../../models/Question'
import TotalTimeInput from '../HandleTest/TotalTimeInput'
import {FileType, QuestionComplexityType, QuestionType} from '../../../common/enums'
import {AnswersHandling} from './AnswerHandling'
import Answer from '../../../models/Answer'
import Program from '../../../models/Program'
import IQuestionService from '../../../services/QuestionService'
import {inject} from '../../../infrastructure/di/inject'
import IFileService from '../../../services/FileService'
import FileModel from '../../../models/FileModel'
import MaterialSelect from '../../Table/MaterialSelect'
import Material, {IMaterialAnchor} from '../../../models/Material'
import Block from '../../Blocks/Block'
import INotificationService from '../../../services/NotificationService'
import {MtBlock} from '../../stuff/Margin'
import Input from '../../stuff/Input'
import Button from '../../stuff/Button'

// @ts-ignore
import decode from 'decode-html'

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
          ...data,
          Text: decode(data.Text)
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

    /*let imageSrc = ((): string | false => {
     const {Image} = this.state.Model

     if (Image && Image.Path)
     return `${window.location.origin}/${Image.Path}`

     return false
     })()

     let getFileHandler = () => imageSrc !== false
     ? (handleAdd: ChangeEventHandler, extensions: string[]) => {
     if (!extensions) return <></>
     const id = Guid.create()
     return <>
     <FileInput extensions={extensions} id={id} onChange={handleAdd}/>
     <label htmlFor={id} style={{width: '100%'}}>
     <Button mainColor='blue' className={classes.addPhotoButton}>
     <PhotoCameraIcon/>
     </Button>
     </label>
     </>
     }
     : (handleAdd: ChangeEventHandler, extensions: string[]) => {
     if (!extensions) return <></>
     return <>
     <FileInput extensions={extensions} id={Guid.create()} onChange={handleAdd}/>
     <FormControl fullWidth>
     <InputLabel shrink htmlFor='Text'>
     Изображение:
     </InputLabel>
     <Button mainColor='blue' className={classes.addPhotoButton}>
     <PhotoCameraIcon/>
     </Button>
     </FormControl>
     </>
     }*/

    let HandledInputs = () => <>
      <Grid item xs={3} md={2}>
        <TotalTimeInput name='Time' label='Длительность:'
                        value={this.state.Model.Time} onChange={this.handleModel}
                        type='duration'
                        mask={[/[0-9]/, /[0-9]/, ':', /[0-5]/, /[0-9]/]}
        />
      </Grid>
      <Grid item xs={6} md={7}>
        <FormControl fullWidth margin='normal'>
          <InputLabel htmlFor='Type'>Тип:</InputLabel>
          <Select name='Type' value={this.state.Model.Type} onChange={this.handleType} input={<Input id='Type'/>}>
            <MenuItem value={QuestionType.ClosedOneAnswer}>Закрытый с одним правильным ответом</MenuItem>
            <MenuItem value={QuestionType.ClosedManyAnswers}>Закрытый с несколькими правильными ответами</MenuItem>
            <MenuItem value={QuestionType.OpenedOneString}>Открытый однострочный</MenuItem>
            <MenuItem value={QuestionType.OpenedManyStrings}>Открытый многострочный</MenuItem>
            <MenuItem value={QuestionType.WithProgram}>Программный код</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      <Grid item xs={3}>
        <FormControl fullWidth margin='normal'>
          <InputLabel htmlFor='Complexity'>Сложность:</InputLabel>
          <Select name='Complexity' value={this.state.Model.Complexity} onChange={this.handleModel}
                  input={<Input id='Complexity'/>}>
            <MenuItem value={QuestionComplexityType.Low}>Лёгкий</MenuItem>
            <MenuItem value={QuestionComplexityType.Medium}>Средний</MenuItem>
            <MenuItem value={QuestionComplexityType.High}>Сложный</MenuItem>
          </Select>
        </FormControl>
      </Grid>
    </>

    return <Grid container justify='center'>
      <Grid item xs={12}>
        <Block partial>
          <Grid item xs={12} container className={classes.header} zeroMinWidth wrap='nowrap'>
            <Typography variant='subtitle1' className={classes.headerText} noWrap>
              {this.state.Model.Id ? 'Редактирование' : 'Создание'} вопроса
            </Typography>
          </Grid>
          <MtBlock value={3}/>
          <Grid item xs={12} container className={classes.inputsBlock} spacing={8}>
            <Grid item xs={12}>
              <FormControl fullWidth>
                <InputLabel shrink htmlFor='Text'>
                  Текст:
                </InputLabel>
                <Input
                  value={this.state.Model.Text}
                  name='Text'
                  onChange={this.handleModel}
                  multiline
                  rows={5}
                  fullWidth
                />
              </FormControl>
            </Grid>
          </Grid>
          <MtBlock value={2}/>
          <Grid item xs={12} container className={classes.inputsBlock} spacing={8}>
            <HandledInputs/>
          </Grid>
          <Grid item xs={12} container>
            <AnswersHandling type={this.state.Model.Type}
                             incomingAnswers={this.state.Model.Answers}
                             handleAnswers={this.handleAnswers}
                             handleProgram={this.handleProgram}
                             inputsBlockStyles={classes.inputsBlock}
            />
          </Grid>
          <MtBlock value={4}/>
          <Grid item xs={12} container>
            <Grid item xs={12}>
              <MaterialSelect
                onSelectMaterial={this.handleMaterialSelect}
                selectedMaterial={this.state.Model.Material}
                handleSelectAnchors={this.handleSelectAnchors}
                selectedAnchors={this.state.Model.MaterialAnchors}
              />
            </Grid>
          </Grid>
          <MtBlock value={4}/>
          <Grid item xs={12} container>
            <Button mainColor='blue' onClick={this.handleSubmit} variant='outlined'>
              {this.state.Model.Id ? 'Обновить' : 'Добавить'}
            </Button>
          </Grid>
        </Block>
      </Grid>
    </Grid>
  }
}

export default withStyles(QuestionHandlingStyle)(QuestionHandling) as any