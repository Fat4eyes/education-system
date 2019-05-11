import {getResult, IResult} from './IServices'
import ProtectedFetch from '../helpers/ProtectedFetch'
import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'
import Question from '../models/Question'

const routes = {
  get: (id: number) => `/api/tests/${id}/process/question`,
  process: (id: number) => `/api/tests/${id}/process/question`
}

export default interface ITestProcessService {
  process(id: number, question: Question): Promise<IResult<Question>>

  get(id: number): Promise<IResult<Question>>
}

export class TestProcessService implements ITestProcessService {
  @inject private NotificationService?: INotificationService

  async get(id: number): Promise<IResult<Question>> {
    let result = await ProtectedFetch.get(routes.get(id))
    if (result === true) 
      result = undefined
    
    return getResult(result)
  }

  async process(id: number, question: Question): Promise<IResult<Question>> {
    return getResult(await ProtectedFetch.post(routes.get(id), JSON.stringify(question)))
  }
}