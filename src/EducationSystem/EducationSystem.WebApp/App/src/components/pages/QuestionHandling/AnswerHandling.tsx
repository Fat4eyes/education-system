import * as React from 'react'
import {ChangeEvent, Component, Fragment} from 'react'
import {LanguageType, QuestionType} from '../../../common/enums'
import Answer from '../../../models/Answer'
import AddIcon from '@material-ui/icons/Add'
import ClearIcon from '@material-ui/icons/Clear'
import MonacoEditor from 'react-monaco-editor'
import Program from '../../../models/Program'
import ProgramData from '../../../models/ProgramData'
import {MtBlock} from '../../stuff/Margin'
import Input from '../../stuff/Input'
import {
  Divider,
  FormControl,
  Grid,
  IconButton,
  InputLabel,
  MenuItem,
  Select,
  Switch,
  Typography
} from '@material-ui/core'
import Button from '../../stuff/Button'

interface TAnswersHandlingProps {
  type: QuestionType,
  incomingAnswers: Array<Answer>
  handleAnswers: (answers: Array<Answer>) => void
  handleProgram: (program: Program) => void
  inputsBlockStyles: any
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
    return <Grid item xs={12} container className={this.props.inputsBlockStyles} spacing={8}>
      <MtBlock value={2}/>
      <Grid item xs={12} container zeroMinWidth wrap='nowrap'>
        <FormControl fullWidth>
          <InputLabel shrink htmlFor='Text'>
            Варианты ответов:
          </InputLabel>
          <Input
            value={this.state.Text}
            name='Text'
            onChange={this.handleInput}
            fullWidth
          />
        </FormControl>
        <IconButton onClick={this.handleAddAnswer} style={{marginTop: 20}}>
          <AddIcon/>
        </IconButton>
      </Grid>
      <MtBlock value={2}/>
      {this.state.Answers.map((answer: Answer, index: number) => <Fragment key={index}>
          <Grid item xs={12} container
                zeroMinWidth wrap='nowrap'
                className={this.props.inputsBlockStyles} spacing={8}
                alignItems='center'
          >
            <Grid item xs={1}>
              <Typography noWrap variant='subtitle1'>
                {index + 1}
              </Typography>
            </Grid>
            <Grid item xs container zeroMinWidth wrap='nowrap'>
              <Typography noWrap variant='subtitle1'>
                {answer.Text}
              </Typography>
            </Grid>
            {
              this.props.type !== QuestionType.OpenedOneString &&
              <Grid item>
                <Switch checked={!!answer.IsRight} onChange={() => this.handleChangeAnswer(index)}/>
              </Grid>
            }
            <Grid item>
              <IconButton onClick={() => this.handleDeleteAnswer(index)}>
                <ClearIcon/>
              </IconButton>
            </Grid>
          </Grid>
          {
            index < this.state.Answers.length - 1 && <>
              <MtBlock value={0.5}/>
              <Divider style={{width: '100%'}}/>
              <MtBlock value={0.5}/>
            </>
          }
        </Fragment>
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
      this.handleModel({
        target: {
          name: 'ProgramDatas',
          value: [...this.state.Model.ProgramDatas, programData]
        }
      }, true)
    }
  }
  handleDeleteParametrs = (index: number) => {
    this.state.Model.ProgramDatas.splice(index, 1)
    this.handleModel({
      target: {
        name: 'ProgramDatas',
        value: [...this.state.Model.ProgramDatas]
      }
    }, true)
  }

  render(): React.ReactNode {
    return <Grid item xs={12}>
      <Grid item xs={12} container className={this.props.inputsBlockStyles} spacing={8}>
        <MtBlock value={2}/>
        <Grid item xs={3}>
          <FormControl fullWidth>
            <InputLabel shrink htmlFor='TimeLimit'>
              Лимит времени:
            </InputLabel>
            <Input
              value={this.state.Model.TimeLimit}
              name='TimeLimit'
              onChange={this.handleModel}
              fullWidth
            />
          </FormControl>
        </Grid>
        <Grid item xs={3}>
          <FormControl fullWidth>
            <InputLabel shrink htmlFor='MemoryLimit'>
              Лимит памяти:
            </InputLabel>
            <Input
              value={this.state.Model.MemoryLimit}
              name='MemoryLimit'
              onChange={this.handleModel}
              fullWidth
            />
          </FormControl>
        </Grid>
        <Grid item xs={6}>
          <FormControl fullWidth required>
            <InputLabel htmlFor='LanguageType'>Язык программирования</InputLabel>
            <Select name='LanguageType'
                    value={this.state.Model.LanguageType}
                    onChange={(e) => this.handleModel(e)}
                    input={<Input id='Type'/>}
            >
              <MenuItem value={LanguageType.CPP}>C++</MenuItem>
              <MenuItem value={LanguageType.PHP}>{LanguageType[LanguageType.PHP]}</MenuItem>
              <MenuItem value={LanguageType.Pascal}>{LanguageType[LanguageType.Pascal]}</MenuItem>
              <MenuItem value={LanguageType.JavaScript}>{LanguageType[LanguageType.JavaScript]}</MenuItem>
            </Select>
          </FormControl>
        </Grid>
      </Grid>
      <MtBlock value={2}/>
      <Grid item xs={12} container className={this.props.inputsBlockStyles} spacing={8}>
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
      <MtBlock value={2}/>
      <Grid item xs={12} container className={this.props.inputsBlockStyles} spacing={8}>
        <Grid item xs={6}>
          <FormControl fullWidth>
            <InputLabel shrink htmlFor='Input'>
              Входные параметры:
            </InputLabel>
            <Input
              value={this.state.Input}
              name='Input'
              onChange={this.handleInput}
              multiline
              rows={4}
              fullWidth
            />
          </FormControl>
        </Grid>
        <Grid item xs={6}>
          <FormControl fullWidth>
            <InputLabel shrink htmlFor='ExpectedOutput'>
              Выходной параметр:
            </InputLabel>
            <Input
              value={this.state.ExpectedOutput}
              name='ExpectedOutput'
              onChange={this.handleInput}
              multiline
              rows={4}
              fullWidth
            />
          </FormControl>
        </Grid>
        <MtBlock value={2}/>
        <Grid item xs={12}>
          <Button mainColor='blue' onClick={this.handleAddParametrs}>Добавить параметры</Button>
        </Grid>
      </Grid>
      <MtBlock value={2}/>
      <Grid item xs={12} container className={this.props.inputsBlockStyles} spacing={8}>
        {this.state.Model.ProgramDatas.map((programData: ProgramData, index: number) =>
          <Fragment key={index}>
            <Grid item xs={12} container 
                  zeroMinWidth wrap='nowrap' className={this.props.inputsBlockStyles} spacing={8}
                  alignItems='center'
            >
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
            {
              index < this.state.Model.ProgramDatas.length - 1 && <>
                <MtBlock value={0.5}/>
                <Divider style={{width: '100%'}}/>
                <MtBlock value={0.5}/>
              </>
            }
          </Fragment>
        )}
      </Grid>
    </Grid>

  }
}