import * as React from 'react'
import {Fragment, FunctionComponent, ReactNode} from 'react'
import {
  Checkbox,
  createStyles, FormControl,
  Grid, InputLabel,
  Radio,
  TextField,
  Theme,
  Typography,
  withStyles,
  WithStyles
} from '@material-ui/core'
import Question from '../../../../models/Question'
import {AnswerStatus, CodeRunStatus, LanguageType, QuestionType} from '../../../../common/enums'
import Answer from '../../../../models/Answer'
import {Guid} from '../../../../helpers/guid'
import {green, orange, red} from '@material-ui/core/colors'
import classNames from 'classnames'
import MonacoEditor from 'react-monaco-editor'
import {MtBlock} from '../../../stuff/Margin'
import {GridSize} from '@material-ui/core/Grid'
import {ICodeRunResult} from '../../../../models/Program'
import Input from '../../../stuff/Input'

const styles = (theme: Theme) => createStyles({
  root: {},
  isRight: {
    color: green[500]
  },
  isIgnored: {
    color: orange[500]
  },
  isWrong: {
    color: red[500]
  },
  image: {
    '&>img': {
      maxWidth: '100%',
      height: 'auto'
    }
  }
})

interface IProps {
  model: Question,
  setAnswer: (value: boolean | string, id?: number) => void
  mode: boolean
}

type TProps = IProps & WithStyles<typeof styles>

const AnswerTextBlock = ({text, xs, ...props}: { text: string | number, xs?: GridSize, [propName: string]: any }) =>
  <Grid item xs={xs} container zeroMinWidth wrap='nowrap'>
    <Typography align='left' color='inherit' {...props}>
      {text}
    </Typography>
  </Grid>

const QuestionBlock = withStyles(styles)((
  ({classes, ...props}: TProps) => {
    const {model} = props

    const answerBlock = () => {
      switch (model.Type) {
        case QuestionType.ClosedManyAnswers:
          return <ClosedManyAnswers {...props}/>
        case QuestionType.ClosedOneAnswer:
          return <ClosedOneAnswer {...props}/>
        case QuestionType.OpenedOneString:
          return <OpenedOneString {...props}/>
        case QuestionType.WithProgram:
          return <WithProgram {...props}/>
      }
    }

    return <Grid container>
      <AnswerTextBlock text={model.Text} xs={12}/>
      {
        model.Image && <>
          <MtBlock/>
          <Grid item xs={3}>
            <a href={'/' + model.Image.Path} target='_blank' className={classes.image}>
              <img src={'/' + model.Image.Path} alt={model.Image.Name}/>
            </a>
          </Grid>
        </>
      }
      <MtBlock/>
      {answerBlock()}
    </Grid>
  }
) as FunctionComponent<IProps>)


type TAnswerControl = {
  children: (answer: Answer) => ReactNode
}

const ClosedAnswersBase = withStyles(styles)((
  ({classes, model, mode, children}: TProps & TAnswerControl) => {
    return <>
      {model.Answers.map((answer: Answer, index: number) =>
        <Grid item xs={12} container alignItems='center' key={index}>
          <Grid item>
            {children(answer)}
          </Grid>
          <Grid item xs>
            <AnswerTextBlock text={answer.Text} className={classNames({
              [classes.isRight]: !mode && answer.Status === AnswerStatus.Right,
              [classes.isWrong]: !mode && answer.Status === AnswerStatus.Wrong,
              [classes.isIgnored]: !mode && answer.Status === AnswerStatus.Ignore
            })}/>
          </Grid>
        </Grid>
      )}
    </>
  }
) as FunctionComponent<TProps & TAnswerControl>)

const ClosedManyAnswers = withStyles(styles)((
  (props: TProps) =>
    <ClosedAnswersBase{...props}>
      {
        (answer: Answer) =>
          <Checkbox
            color='primary'
            checked={!!answer.IsRight}
            onChange={({target: {checked}}) => props.setAnswer(checked, answer.Id)}
            disabled={!props.mode}
          />
      }
    </ClosedAnswersBase>
) as FunctionComponent<TProps>)

const ClosedOneAnswer = withStyles(styles)((
  (props: TProps) => {
    const radioName = Guid.create()

    return <ClosedAnswersBase{...props}>
      {
        (answer: Answer) =>
          <Radio
            checked={!!answer.IsRight}
            onChange={({target: {checked}}) => props.setAnswer(checked, answer.Id)}
            name={radioName}
            disabled={!props.mode}
            color='primary'
          />
      }
    </ClosedAnswersBase>
  }
) as FunctionComponent<TProps>)

const OpenedOneString = withStyles(styles)((
  ({model, setAnswer, mode, classes}: TProps) => {
    const answer = model.Answers.length ? model.Answers[0] : new Answer('')
    return <Grid item xs={12} container alignItems='center'>
      <FormControl fullWidth>
        <InputLabel shrink htmlFor='Text'>
          Ответ:
        </InputLabel>
        <Input
          autoFocus={true}
          value={answer.Text}
          name='Text'
          onChange={({target: {value}}) => setAnswer(value)}
          fullWidth
          disabled={!mode}
          className={classNames({
            [classes.isRight]: answer.Status === AnswerStatus.Right,
            [classes.isWrong]: answer.Status === AnswerStatus.Wrong
          })}
        />
      </FormControl>
    </Grid>
  }
) as FunctionComponent<TProps>)

const WithProgram = withStyles(styles)((
  ({model, setAnswer, mode, classes}: TProps) => {
    const {Program: program} = model
    if (!program) return <></>

    let codeExecutionResult = program.CodeRunningResult ? program.CodeRunningResult.CodeExecutionResult : false

    return <Grid item xs={12} container>
      {
        !mode && codeExecutionResult && <>
          <MtBlock value={2}/>
          <AnswerTextBlock text={codeExecutionResult.Success ? 'Правильно' : 'Неправильно'} xs={12}/>
          {
            !!codeExecutionResult.Errors.length && <>
              <MtBlock value={2}/>
              <AnswerTextBlock text='Ошибки:' xs={12}/>
              <MtBlock value={1}/>
              {codeExecutionResult.Errors.map((error: string, index: number) =>
                <Grid item xs={12} container key={index}>
                  <AnswerTextBlock text={index + 1} xs={1}/>
                  <AnswerTextBlock text={error} xs={11} key={index} className={classes.isWrong}/>
                </Grid>
              )}
            </>
          }
          <MtBlock value={2}/>
          {
            codeExecutionResult.Results.length && <>
              <AnswerTextBlock text='Тестовые кейсы:' xs={12}/>
              <MtBlock/>
              <Grid item xs={12} container>
                <AnswerTextBlock text='Пользовательские данные' xs={6}/>
                <AnswerTextBlock text='Ожидаемые данные' xs={6}/>
                <MtBlock/>
                {codeExecutionResult.Results.map((runResult: ICodeRunResult, index: number) =>
                  <Fragment key={index}>
                    <Grid item xs={12} container>
                      <AnswerTextBlock text={index + 1} xs={1}/>
                      <AnswerTextBlock text={runResult.UserOutput} xs={5} className={classNames({
                        [classes.isRight]: runResult.Success,
                        [classes.isWrong]: !runResult.Success
                      })}/>
                      <AnswerTextBlock text={runResult.ExpectedOutput} xs={6} className={classNames({
                        [classes.isRight]: runResult.Success,
                        [classes.isWrong]: !runResult.Success
                      })}/>
                    </Grid>
                    {
                      runResult.Status === CodeRunStatus.MemoryExcess && <>
                        <Grid item xs={1}/>
                        <AnswerTextBlock text='Превышен лимит по памяти' xs={6} className={classes.isWrong}/>
                      </>
                    }
                    {
                      runResult.Status === CodeRunStatus.TimeExcess && <>
                        <Grid item xs={1}/>
                        <AnswerTextBlock text='Превышен лимит по памяти' xs={6} className={classes.isWrong}/>
                      </>
                    }
                    <MtBlock value={2}/>
                  </Fragment>
                )}
              </Grid>
            </>
          }
        </>
      }
      <MtBlock value={2}/>
      <MonacoEditor
        height={mode ? 500 : 250}
        language={LanguageType[program.LanguageType!].toLowerCase()}
        value={program.Source}
        options={{
          selectOnLineNumbers: true,
          roundedSelection: false,
          cursorStyle: 'line',
          automaticLayout: false,
          readOnly: !mode
        }}
        onChange={code => setAnswer(code)}
      />
    </Grid>
  }
) as FunctionComponent<TProps>)


export default QuestionBlock as FunctionComponent<IProps>