import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import {
  Button,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Paper,
  Select,
  TextField,
  withStyles,
  WithStyles
} from '@material-ui/core'
import QuestionHandlingStyle from './QuestionHandlingStyle'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import Question from '../../../models/Question'
import TotalTimeInput from '../HandleTest/TotalTimeInput'
import {QuestionComplexityType, QuestionType} from '../../../common/enums'
import {AnswersHandling} from './AnswerHandling'
import Answer from '../../../models/Answer'
import Program from '../../../models/Program'
import IQuestionService from '../../../services/abstractions/IQuestionService'
import {inject} from '../../../infrastructure/di/inject'
import {Exception} from '../../../helpers'

interface IProps {
  match: {
    params: {
      themeId: number
    }
  }
}

type TProps = WithStyles<typeof QuestionHandlingStyle> & InjectedNotistackProps & IProps

interface IState {
  Model: Question
}

class QuestionHandling extends Component<TProps, IState> {
  @inject
  private QuestionService?: IQuestionService

  constructor(props: TProps) {
    super(props)

    this.state = {
      Model: new Question()
    }
    this.state.Model.ThemeId = this.props.match.params.themeId
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
    let result = await this.QuestionService!.add(this.state.Model)

    if (result instanceof Exception) {
      return this.props.enqueueSnackbar(result.message, {
        variant: 'error',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })
    }

    if ((result as Question).Id) {
      this.props.enqueueSnackbar(`Вопрос успешно добавлен`, {
        variant: 'success',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })

      this.setState({
        Model: new Question()
      })
    }
  }

  render(): React.ReactNode {
    let {classes} = this.props

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
        <Paper className={classes.paper}>
          <Grid item xs={12} container spacing={16}>
            <HandledInputs/>
          </Grid>
          <Grid item xs={12} container spacing={16}>
            <Grid item xs={12}>
              <TextField name='Text' label='Текст вопроса' required multiline fullWidth rows={5}
                         value={this.state.Model.Text} onChange={this.handleModel}/>
            </Grid>
          </Grid>
          <Grid item xs={12} container spacing={16}>
            <AnswersHandling type={this.state.Model.Type}
                             incomingAnswers={this.state.Model.Answers}
                             handleAnswers={this.handleAnswers}
                             handleProgram={this.handleProgram}
            />
          </Grid>
          <Grid item xs={12} container spacing={16}>
            <Button onClick={this.handleSubmit}>
              Добавить вопрос
            </Button>
          </Grid>
        </Paper>
      </Grid>
    </Grid>
  }
}

export default withSnackbar(withStyles(QuestionHandlingStyle)(QuestionHandling) as any)