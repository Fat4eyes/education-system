import {
  ClosedManyAnswersStrategy,
  ClosedOneAnswerStrategy,
  getHandleQuestionStrategy,
  WithProgramStrategy
} from './HandleQuestionStrategy'
import {QuestionType} from '../../../../common/enums'
import Question from '../../../../models/Question'
import Program from '../../../../models/Program'
import Answer from '../../../../models/Answer'

describe('getHandleQuestionStrategy', () => {
  it('should get with program strategy', () => {
    const element = getHandleQuestionStrategy(QuestionType.WithProgram)
    expect(element).toEqual(WithProgramStrategy)
  })
})

describe('getHandleQuestionStrategy', () => {
  it('should get with program strategy', () => {
    const element = getHandleQuestionStrategy(QuestionType.WithProgram)
    expect(element).toEqual(WithProgramStrategy)
  })
})

describe('WithProgramStrategyPreprocess', () => {
  it('should get correctly preprocess question with program', () => {
    const question = <Question>{
      Program: <Program>{
        Template: 'WithProgramStrategyPreprocess',
        Source: ''
      }
    }

    const result = WithProgramStrategy.preprocess(question)
    expect(result).toEqual(<Question>{
      Program: <Program>{
        Template: 'WithProgramStrategyPreprocess',
        Source: 'WithProgramStrategyPreprocess'
      }
    })
  })
})

describe('ClosedOneAnswerStrategyProcess', () => {
  it('should get correctly process question answers', () => {
    const question = <Question>{
      Answers: [
        <Answer>{Id: 1, IsRight: false},
        <Answer>{Id: 2, IsRight: false},
        <Answer>{Id: 3, IsRight: true}
      ]
    }

    const result = ClosedOneAnswerStrategy.process(question, true, 2)
    expect(result).toEqual(<Question>{
      Answers: [
        <Answer>{Id: 1, IsRight: false},
        <Answer>{Id: 2, IsRight: true},
        <Answer>{Id: 3, IsRight: false}
      ]
    })
  })
})

describe('ClosedManyAnswerStrategyProcess', () => {
  it('should get correctly process question answers', () => {
    const question = <Question>{
      Answers: [
        <Answer>{Id: 1, IsRight: false},
        <Answer>{Id: 2, IsRight: false},
        <Answer>{Id: 3, IsRight: true}
      ]
    }

    const result = ClosedManyAnswersStrategy.process(question, true, 2)
    expect(result).toEqual(<Question>{
      Answers: [
        <Answer>{Id: 1, IsRight: false},
        <Answer>{Id: 2, IsRight: true},
        <Answer>{Id: 3, IsRight: true}
      ]
    })
  })
})