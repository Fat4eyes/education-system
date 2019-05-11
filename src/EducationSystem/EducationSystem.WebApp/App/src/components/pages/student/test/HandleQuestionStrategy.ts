import Question from '../../../../models/Question'
import {QuestionType} from '../../../../common/enums'
import Answer from '../../../../models/Answer'

export interface IHandleQuestionStrategy {
  handleQuestion(question: Question, answer: boolean | string, id?: number): Question
  prepareQuestion(question: Question): Question
}

export const NullHandleQuestionStrategy: IHandleQuestionStrategy = {
  handleQuestion: (question: Question): Question => question,
  prepareQuestion: (question: Question): Question => question
}

const ClosedManyAnswersStrategy: IHandleQuestionStrategy = {
  handleQuestion(question: Question, answer: boolean | string, id?: number): Question {
    if (!id) return question
    let current = question.Answers.find(a => a.Id === id)
    if (current) current.IsRight = !!answer
    return {...question}
  },
  prepareQuestion(question: Question): Question {
    return {
      ...question,
      Answers: question.Answers.filter(a => a.IsRight === true)
    }
  }
}

const ClosedOneAnswerStrategy: IHandleQuestionStrategy = {
  ...ClosedManyAnswersStrategy,
  handleQuestion(question: Question, answer: boolean | string, id?: number): Question {
    if (!id) return question
    question.Answers.forEach(a => a.IsRight = a.Id === id ? !!answer : false)
    return {...question}
  }
}

const OpenedOneStringStrategy: IHandleQuestionStrategy = {
  ...NullHandleQuestionStrategy,
  handleQuestion(question: Question, answer: boolean | string): Question {
    if (!question.Answers[0]) question.Answers.push(new Answer(answer.toString()))
    else question.Answers[0].Text = answer.toString()
    return {...question}
  }
}

const WithProgramStrategy: IHandleQuestionStrategy = {
  ...NullHandleQuestionStrategy,
  handleQuestion(question: Question, answer: boolean | string): Question {
    let program = question.Program
    if (!program) return {...question}
    program.Template = answer.toString()
    return {...question, Program: {...program}}
  }
}

export const getHandleAnswerStrategy = (type: QuestionType): IHandleQuestionStrategy => {
  switch (type) {
    case QuestionType.ClosedManyAnswers:
      return ClosedManyAnswersStrategy
    case QuestionType.ClosedOneAnswer:
      return ClosedOneAnswerStrategy
    case QuestionType.OpenedOneString:
      return OpenedOneStringStrategy
    case QuestionType.OpenedManyStrings:
      return NullHandleQuestionStrategy
    case QuestionType.WithProgram:
      return WithProgramStrategy
  }
}