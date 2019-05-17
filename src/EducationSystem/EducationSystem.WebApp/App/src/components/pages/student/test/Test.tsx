import {Button, Grid, Typography, WithStyles, withStyles} from '@material-ui/core'
import {TestStyles} from './TestStyles'
import * as React from 'react'
import {Component} from 'react'
import Block from '../../../Blocks/Block'
import {RouteComponentProps} from 'react-router'
import {inject} from '../../../../infrastructure/di/inject'
import TestModel from '../../../../models/Test'
import Question from '../../../../models/Question'
import ITestProcessService from '../../../../services/TestProcessService'
import ITestService from '../../../../services/TestService'
import QuestionBlock from './QuestionBlock'
import {getHandleQuestionStrategy, IHandleQuestionStrategy, NullHandleQuestionStrategy} from './HandleQuestionStrategy'
import {indexOf, shuffle} from '../../../../helpers/ArrayHelpers'
import {MtBlock} from '../../../stuff/Margin'
import MaterialBlock from './MaterialBlock'

type TProps = WithStyles<typeof TestStyles> & RouteComponentProps<{ id: string }>

interface IState {
  Test?: TestModel
  Question?: Question
  Questions: Array<Question>
  IsFinish: boolean
  Strategy: IHandleQuestionStrategy
  Mode?: boolean
  NeedSave: boolean,
  ShowMaterial: boolean
}

class Test extends Component<TProps, IState> {
  @inject private TestProcessService?: ITestProcessService
  @inject private TestService?: ITestService

  constructor(props: TProps) {
    super(props)

    this.state = {
      IsFinish: false,
      Strategy: NullHandleQuestionStrategy,
      NeedSave: true,
      Questions: [],
      ShowMaterial: false
    }
  }

  getQuestion = async () => {
    let {Test, Questions, Question} = this.state
    if (!Test) return

    let questions = [...Questions]
    if (Questions.length === 0 || Question && indexOf(Questions, q => q.Id === Question!.Id) === Questions.length - 1) {
      const {data, success} = await this.TestProcessService!.getQuestions(Test.Id!)

      if (success) {
        if (!data || data.Count === 0) {
          return this.setState({IsFinish: true})
        } else {
          data.Items.sort((a, b) => a.Id !== undefined && b.Id !== undefined && a.Id > b.Id ? 1 : -1)
          questions = data.Items
        }
      }
    }

    let index = 0
    if (Question) {
      let oldIndex = indexOf(questions, q => q.Id === Question!.Id)
      if (oldIndex !== -1) index = oldIndex + 1
      if (oldIndex === questions.length - 1) {
        index = 0
        shuffle(questions)
      }
    }

    let newQuestion = questions.find(q => q.Material !== undefined) || questions[index]
    let strategy = getHandleQuestionStrategy(newQuestion.Type)

    this.setState({
      Question: strategy.preprocess(newQuestion),
      Questions: questions,
      Strategy: strategy,
      Mode: true,
      ShowMaterial: false
    })
  }

  checkQuestion = async () => {
    if (!this.state.Test || !this.state.Question) return

    let question: Question = {
      ...this.state.Strategy.postprocess(this.state.Question),
      Save: this.state.NeedSave
    }

    const {data, success} = await this.TestProcessService!.process(this.state.Test.Id!, question)

    if (success && data) {
      let answers = this.state.Question!.Answers

      if (data.Answers) {
        answers.forEach(a => {
          let newAnswer = data.Answers.find(na => na.Id === a.Id)
          if (newAnswer) {
            a.Status = newAnswer.Status
          }
        })
      }

      data.Answers = answers

      this.setState({
        Question: data,
        Mode: false
      })
    }
  }

  async componentDidMount() {
    const {data, success} = await this.TestService!.get(Number(this.props.match.params.id))

    if (success && data) {
      this.setState({
        Test: data
      }, this.getQuestion)
    }
  }

  handleAnswer = (value: boolean | string, id?: number) => {
    if (!this.state.Question || this.state.IsFinish) return
    this.setState({Question: this.state.Strategy.process(this.state.Question, value, id)})
  }
  
  handleMaterial = () => this.setState(state => ({ShowMaterial: !state.ShowMaterial}))

  render(): React.ReactNode {
    let {classes} = this.props

    return <Grid container justify='center' spacing={40}>
      <Grid item xs={12} lg={10}>
        <Block partial>
          {
            this.state.IsFinish &&
            <Grid item xs={12} container zeroMinWidth wrap='nowrap'>
              <Typography align='center' color='inherit'>
                Тест пройден
              </Typography>
            </Grid>

          }
          {
            this.state.Question && <>
              <Grid item xs={12}>
                <QuestionBlock model={this.state.Question} setAnswer={this.handleAnswer} mode={this.state.Mode!}/>
              </Grid>
              <MtBlock value={2}/>
              <Grid item xs={12} container>
                {this.state.Mode === true && <Button onClick={this.checkQuestion} variant='outlined'>Проверить</Button>}
                {this.state.Mode === false && <Button onClick={this.getQuestion} variant='outlined'>Следующий</Button>}
                <Grid item xs/>
                {
                  this.state.Question.Material && 
                  <Button onClick={this.handleMaterial} variant='outlined'>
                    {this.state.ShowMaterial ? 'Скрыть' : 'Показать'} материал
                  </Button>
                }
              </Grid>
            </>
          }
        </Block>
      </Grid>
      {
        this.state.ShowMaterial && this.state.Question && this.state.Question.Material &&
        <Grid item xs={12} lg={10}>
            <MaterialBlock model={this.state.Question.Material}/>
        </Grid>
      }
    </Grid>
  }
}

export default withStyles(TestStyles)(Test) as any