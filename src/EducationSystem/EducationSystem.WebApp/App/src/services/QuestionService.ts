import Question from '../models/Question'
import ProtectedFetch from '../helpers/ProtectedFetch'
import {getResult, IResult} from './IServices'
import IPagedData, {IPagingOptions} from '../models/PagedData'
import UrlBuilder from '../helpers/UrlBuilder'
import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'

const routes = {
  get: (id: number) => `/api/questions/${id}`,
  add: () => '/api/questions',
  update: (id: number) => `/api/questions/${id}`,
  delete: (id: number) => `/api/questions/${id}`,
  getByThemeId: (id: number) => `/api/themes/${id}/questions`,
  setOrder: (id: number) => `/api/themes/${id}/questions`
}

export default interface IQuestionService {
  add(question: Question): Promise<IResult<number>>
  update(question: Question): Promise<boolean>
  get(id: number): Promise<IResult<Question>>
  getByThemeId(themeId: number, options: IPagingOptions): Promise<IResult<IPagedData<Question>>>
  setOrder(themeId: number, questions: Array<Question>): Promise<boolean>
  delete(id: number): Promise<boolean>
}

export class QuestionService implements IQuestionService {
  @inject private NotificationService?: INotificationService
  
  async add(question: Question): Promise<IResult<number>> {
    //TODO validation
    return getResult(await ProtectedFetch.post(routes.add(), JSON.stringify(question)))
  }

  async get(id: number): Promise<IResult<Question>> {
    return getResult(await ProtectedFetch.get(routes.get(id)))
  }

  async update(question: Question): Promise<boolean> {
    //TODO validation
    return !!(await ProtectedFetch.put(routes.update(question.Id!), JSON.stringify(question)))
  }

  async delete(id: number): Promise<boolean> {
    if (!!(await ProtectedFetch.delete(routes.delete(id)))) {
      this.NotificationService!.showSuccess('Вопрос успешно удален')
      return true
    }
    return false
  }

  async getByThemeId(themeId: number, options: IPagingOptions): Promise<IResult<IPagedData<Question>>> {
    return getResult(await ProtectedFetch.get(
      UrlBuilder.Build(routes.getByThemeId(themeId), options)
    ))
  }

  async setOrder(themeId: number, questions: Array<Question>): Promise<boolean> {
    return await ProtectedFetch.put(
      routes.setOrder(themeId),
      JSON.stringify(questions)
    )
  }
}