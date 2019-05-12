import Question from '../../../../models/Question'
import {QuestionType} from '../../../../common/enums'
import Answer from '../../../../models/Answer'
import {shuffle} from '../../../../helpers/ArrayHelpers'

export interface IHandleQuestionStrategy {
  process(question: Question, answer: boolean | string, id?: number): Question
  preprocess(question: Question): Question
  postprocess(question: Question): Question
}
  
export const NullHandleQuestionStrategy: IHandleQuestionStrategy = {
  process: (question: Question): Question => question,
  preprocess: (question: Question): Question => question,
  postprocess: (question: Question): Question => question
}

const ClosedManyAnswersStrategy: IHandleQuestionStrategy = {
  process(question: Question, answer: boolean | string, id?: number): Question {
    if (!id) return question
    let current = question.Answers.find(a => a.Id === id)
    if (current) current.IsRight = !!answer
    return {...question}
  },
  preprocess(question: Question): Question {
    if (!question.Answers) question.Answers = []
    else shuffle(question.Answers)
    return {...question}
  },
  postprocess(question: Question): Question {
    return {
      ...question,
      Answers: question.Answers.filter(a => a.IsRight === true)
    }
  }
}

const ClosedOneAnswerStrategy: IHandleQuestionStrategy = {
  ...ClosedManyAnswersStrategy,
  process(question: Question, answer: boolean | string, id?: number): Question {
    if (!id) return question
    question.Answers.forEach(a => a.IsRight = a.Id === id ? !!answer : false)
    return {...question}
  }
}

const OpenedOneStringStrategy: IHandleQuestionStrategy = {
  ...NullHandleQuestionStrategy,
  process(question: Question, answer: boolean | string): Question {
    if (!question.Answers[0]) question.Answers.push(new Answer(answer.toString()))
    else question.Answers[0].Text = answer.toString()
    return {...question}
  }
}

const WithProgramStrategy: IHandleQuestionStrategy = {
  ...NullHandleQuestionStrategy,
  process(question: Question, answer: boolean | string): Question {
    let program = question.Program
    if (!program) return {...question}
    program.Template = answer.toString()
    return {...question, Program: {...program}}
  },
  preprocess(question: Question): Question {
    if (!question.Program) return question
    question.Program.Source = question.Program.Template.toString()
    return {...question}
  }
}

export const getHandleQuestionStrategy = (type: QuestionType): IHandleQuestionStrategy => {
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