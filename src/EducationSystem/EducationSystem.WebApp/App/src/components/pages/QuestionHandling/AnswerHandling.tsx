import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import {LanguageType, QuestionType} from '../../../common/enums'
import {
  Button,
  FormControl,
  Grid,
  IconButton,
  InputLabel,
  MenuItem,
  Select,
  Switch,
  TextField,
  Typography
} from '@material-ui/core'
import Answer from '../../../models/Answer'
import AddIcon from '@material-ui/icons/Add'
import ClearIcon from '@material-ui/icons/Clear'
import MonacoEditor from 'react-monaco-editor'
import Program from '../../../models/Program'
import {VTextField} from '../../core'
import ProgramData from '../../../models/ProgramData'
import {MtBlock} from '../../stuff/Margin'

interface TAnswersHandlingProps {
  type: QuestionType,
  incomingAnswers: Array<Answer>
  handleAnswers: (answers: Array<Answer>) => void
  handleProgram: (program: Program) => void
}

interface IClosedManyAnswersState {
  Text: string
  Answers: Array<Answer>
}

export const AnswersHandling = (props: TAnswersHandlingProps) => {
  switch (props.type) {
    case QuestionType.ClosedManyAnswers:
      return <ClosedManyAnswers {...props}/>
    case QuestionType.ClosedOneAnswer:
      return <ClosedOneAnswer {...props}/>
    case QuestionType.OpenedOneString:
      return <OpenedOneString {...props}/>
    case QuestionType.OpenedManyStrings:
      return <BaseAnswers {...props}/>
    case QuestionType.WithProgram:
      return <WithProgram {...props}/>
  }
}

class BaseAnswers<IState extends IClosedManyAnswersState> extends Component<TAnswersHandlingProps, IState> {
  constructor(props: TAnswersHandlingProps) {
    super(props)

    this.state = {
      Text: '',
      Answers: props.incomingAnswers
    } as IState
  }

  componentWillReceiveProps(nextProps: Readonly<TAnswersHandlingProps>) {
    this.setState(state => ({Answers: [...nextProps.incomingAnswers]}))
  }

  protected handleAnswers = (answers: Array<Answer>, needRefreshText: boolean = false) =>
    this.setState((needRefreshText
      ? {Answers: answers, Text: ''}
      : {Answers: answers}) as IState,
      () => this.props.handleAnswers(answers)
    )

  protected handleInput = ({target: {name, value}}: ChangeEvent<HTMLInputElement>) => {
    // @ts-ignore
    this.setState({[name]: value})
  }

  protected handleAddAnswer = () => {
    let answer = new Answer(this.state.Text)
    this.handleAnswers([...this.state.Answers, answer], true)
  }

  handleDeleteAnswer = (index: number) => {
    this.state.Answers.splice(index, 1)
    this.handleAnswers([...this.state.Answers])
  }

  render(): React.ReactNode {
    return <Grid item xs={12}/>
  }
}

class ClosedManyAnswers extends BaseAnswers<IClosedManyAnswersState> {
  handleAddAnswer = () => {
    let answer = new Answer(this.state.Text, false)
    this.handleAnswers([...this.state.Answers, answer], true)
  }

  protected handleChangeAnswer = (index: number) => {
    this.state.Answers[index].IsRight = !this.state.Answers[index].IsRight
    this.handleAnswers([...this.state.Answers])
  }

  render(): React.ReactNode {
    return <Grid item xs={12}>
      <Grid item xs={12} container zeroMinWidth wrap='nowrap'>
        <TextField
          fullWidth
          label='Варианты ответов'
          required
          name='Text'
          value={this.state.Text}
          onChange={this.handleInput}
        />
        <IconButton onClick={this.handleAddAnswer}>
          <AddIcon/>
        </IconButton>
      </Grid>
      <MtBlock/>
      {this.state.Answers.map((answer: Answer, index: number) =>
        <Grid item xs={12} container zeroMinWidth wrap='nowrap' key={index}>
          <Grid item xs={1}><Typography noWrap variant='subtitle1'>{index + 1}</Typography></Grid>
          <Grid item xs container zeroMinWidth wrap='nowrap'>
            <Typography noWrap variant='subtitle1'>{answer.Text}</Typography>
          </Grid>
          {this.props.type !== QuestionType.OpenedOneString &&
          <Grid item xs={2}>
            <Switch checked={!!answer.IsRight} onChange={() => this.handleChangeAnswer(index)}/>
          </Grid>
          }
          <Grid item>
            <IconButton onClick={() => this.handleDeleteAnswer(index)}>
              <ClearIcon/>
            </IconButton>
          </Grid>
        </Grid>
      )}
    </Grid>
  }
}

class ClosedOneAnswer extends ClosedManyAnswers {
  protected handleChangeAnswer = (index: number) => {
    this.state.Answers.forEach((answer: Answer, i: number) => {
      if (i === index) {
        answer.IsRight = !answer.IsRight
      } else {
        answer.IsRight = false
      }
    })
    this.handleAnswers([...this.state.Answers])
  }
}

class OpenedOneString extends ClosedManyAnswers {
  handleAddAnswer = () => {
    let answer = new Answer(this.state.Text)
    this.handleAnswers([...this.state.Answers, answer], true)
  }
}

interface IWithProgramState {
  Model: Program,
  Input: string,
  ExpectedOutput: string
}

class WithProgram extends Component<TAnswersHandlingProps, IWithProgramState> {
  constructor(props: TAnswersHandlingProps) {
    super(props)

    this.state = {
      Model: new Program()
    } as IWithProgramState
  }

  handleModel = ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any, needRefreshText: boolean = false) => {
    if (needRefreshText) {
      this.setState(state => ({
        Model: {
          ...state.Model,
          [name]: value
        },
        Input: '',
        ExpectedOutput: ''
      }), () => this.props.handleProgram(this.state.Model))
    } else {
      this.setState(state => ({
        Model: {
          ...state.Model,
          [name]: value
        }
      }), () => this.props.handleProgram(this.state.Model))
    }
  }
  handleInput = ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any) => {
    // @ts-ignore
    this.setState({[name]: value})
  }
  handleAddParametrs = () => {
    let {Input, ExpectedOutput} = this.state
    
    if (Input && ExpectedOutput) {
      let programData = new ProgramData(Input, ExpectedOutput)
      this.handleModel({target: {
        name: 'ProgramDatas', 
        value: [...this.state.Model.ProgramDatas, programData]
      }}, true)
    }
  }
  handleDeleteParametrs = (index: number) => {
    this.state.Model.ProgramDatas.splice(index, 1)
    this.handleModel({target: {
        name: 'ProgramDatas',
        value: [...this.state.Model.ProgramDatas]
      }}, true)
  }

  render(): React.ReactNode {
    return <Grid item xs={12}>
      <Grid item xs={12} container spacing={16}>
        <Grid item xs={12} sm={6} md={4} lg={3}>
          <VTextField
            name='TimeLimit'
            type='number'
            label='Лимит времени'
            value={this.state.Model.TimeLimit}
            onChange={this.handleModel}
            margin='normal' required fullWidth
            validators={{
              max: {value: 60, message: '< 60 >'},
              required: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6} md={4} lg={3}>
          <VTextField
            name='MemoryLimit'
            type='number'
            label='Лимит памяти'
            value={this.state.Model.MemoryLimit}
            onChange={this.handleModel}
            margin='normal' required fullWidth
            validators={{
              max: {value: 10000, message: '< 10000 >'},
              required: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6} md={4} lg={3}>
          <FormControl fullWidth margin='normal' required>
            <InputLabel htmlFor='LanguageType'>Язык программирования</InputLabel>
            <Select name='LanguageType' value={this.state.Model.LanguageType} onChange={(e) => this.handleModel(e)}>
              <MenuItem value={LanguageType.CPP}>{LanguageType[LanguageType.CPP]}</MenuItem>
              <MenuItem value={LanguageType.PHP}>{LanguageType[LanguageType.PHP]}</MenuItem>
              <MenuItem value={LanguageType.Pascal}>{LanguageType[LanguageType.Pascal]}</MenuItem>
              <MenuItem value={LanguageType.JavaScript}>{LanguageType[LanguageType.JavaScript]}</MenuItem>
            </Select>
          </FormControl>
        </Grid>
      </Grid>
      <Grid item xs={12} container spacing={16}>
        <MonacoEditor
          height='500'
          language={LanguageType[this.state.Model.LanguageType!].toLowerCase()}
          value={this.state.Model.Template}
          options={{
            selectOnLineNumbers: true,
            roundedSelection: false,
            readOnly: false,
            cursorStyle: 'line',
            automaticLayout: false
          }}
          onChange={value => this.handleModel({target: {name: 'Template', value: value}})}
        />
      </Grid>
      <Grid item xs={12} container spacing={16}>
        <Grid item xs={6}>
          <TextField name='Input' label='Входные параметры' required multiline fullWidth rows={4}
                     value={this.state.Input} onChange={this.handleInput}/>
        </Grid>
        <Grid item xs={6}>
          <TextField name='ExpectedOutput' label='Выходной параметр' required multiline fullWidth rows={4}
                     value={this.state.ExpectedOutput} onChange={this.handleInput}/>
        </Grid>
        <Grid item xs={12}>
          <Button onClick={this.handleAddParametrs}>Добавить параметры</Button>
        </Grid>
      </Grid>
      <Grid item xs={12} container spacing={16}>
        {this.state.Model.ProgramDatas.map((programData: ProgramData, index: number) =>
          <Grid item xs={12} container zeroMinWidth wrap='nowrap' key={index}>
            <Grid item xs={1}><Typography noWrap variant='subtitle1'>{index + 1}</Typography></Grid>
            <Grid item xs container zeroMinWidth wrap='nowrap'>
              <Typography noWrap variant='subtitle1'>{programData.Input}</Typography>
            </Grid>
            <Grid item xs container zeroMinWidth wrap='nowrap'>
              <Typography noWrap variant='subtitle1'>{programData.ExpectedOutput}</Typography>
            </Grid>
            <Grid item>
              <IconButton onClick={() => this.handleDeleteParametrs(index)}>
                <ClearIcon/>
              </IconButton>
            </Grid>
          </Grid>
        )}
      </Grid>
    </Grid>

  }
}