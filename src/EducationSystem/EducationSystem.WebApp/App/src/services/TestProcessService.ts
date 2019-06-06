import {getResult, IResult} from './IServices'
import ProtectedFetch from '../helpers/ProtectedFetch'
import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'
import Question, {IFilterQuestion} from '../models/Question'
import {UrlBuilder} from '../helpers'
import IPagedData from '../models/PagedData'
import {QuestionType} from '../common/enums'
import {ICodeExecutionResult} from '../models/Program'

const routes = {
  getQuestions: (id: number) => `/api/tests/${id}/questions`,
  process: (id: number) => `/api/tests/${id}/question`,
  reset: (id: number) => `/api/tests/${id}/reset`,
  execute: () => '/api/code/execute'
}

export default interface ITestProcessService {
  process(id: number, question: Question): Promise<IResult<Question>>

  getQuestions(id: number, filter?: IFilterQuestion): Promise<IResult<IPagedData<Question>>>

  reset(id: number, question: Question): Promise<boolean>
}

export class TestProcessService implements ITestProcessService {
  @inject private NotificationService?: INotificationService

  async getQuestions(id: number, filter: IFilterQuestion = {
    Take: 20,
    Passed: false
  }): Promise<IResult<IPagedData<Question>>> {
    let result = await ProtectedFetch.get(UrlBuilder.Build(routes.getQuestions(id), filter))
    return getResult(result)
  }

  async process(id: number, question: Question): Promise<IResult<Question>> {
    switch (question.Type) {
      case QuestionType.ClosedManyAnswers:
      case QuestionType.ClosedOneAnswer:
      case QuestionType.OpenedOneString:
      case QuestionType.OpenedManyStrings:
        return getResult(await ProtectedFetch.post(routes.process(id), JSON.stringify(question)))
      case QuestionType.WithProgram:
        if (!question.Program) return getResult()
        let data = await ProtectedFetch.post(routes.process(id), JSON.stringify(question))
        console.log(data)
        if (!data 
          || !data.Program 
          || !data.Program.CodeRunningResult
          || !data.Program.CodeRunningResult.CodeExecutionResult
        ) return getResult()
        
        return getResult(<Question>{
          ...data,
          Program: {
            ...data.Program,
            CodeRunningResult: {
              CodeExecutionResult: {
                Errors: [],
                Results: [],
                ...data.Program.CodeRunningResult.CodeExecutionResult,
              }
            }
          }
        })
    }
  }

  async reset(id: number): Promise<boolean> {
    return !!await ProtectedFetch.post(routes.reset(id))
  }
}