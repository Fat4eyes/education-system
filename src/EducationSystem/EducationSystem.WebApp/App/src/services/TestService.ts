import {getResult, IResult} from './IServices'
import Test from '../models/Test'
import IPagedData, {IPagingOptions} from '../models/PagedData'
import {ITestFilter} from '../models/Filters'
import ProtectedFetch from '../helpers/ProtectedFetch'
import UrlBuilder from '../helpers/UrlBuilder'
import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'

const routes = {
  add: () => '/api/tests',
  get: (id: number) => `/api/tests/${id}`,
  update: (id: number) => `/api/tests/${id}`,
  delete: (id: number) => `/api/tests/${id}`,
  getAll: () => `/api/tests`,
  getByDisciplineId: (id: number) => `/api/disciplines/${id}/tests`,
  resetProcess: (id: number) => `/api/tests/${id}/process`
}

export default interface ITestService {
  add(test: Test): Promise<IResult<number>>
  update(test: Test): Promise<boolean>
  getAll(params: ITestFilter): Promise<IResult<IPagedData<Test>>>
  get(id: number): Promise<IResult<Test>>
  delete(id: number): Promise<boolean>
  getByDisciplineId(disciplineId: number, options: IPagingOptions): Promise<IResult<IPagedData<Test>>>
  resetProcess(id: number): Promise<boolean>
}

export class TestService implements ITestService {
  @inject private NotificationService?: INotificationService
  
  async resetProcess(id: number): Promise<boolean> {
    if (!!(await ProtectedFetch.delete(routes.resetProcess(id)))) {
      this.NotificationService!.showSuccess('Прогресс успешно сброшен')
      return true
    }
    return false
  }
  
  async add(test: Test): Promise<IResult<number>> {
    //TODO validation
    return getResult(await ProtectedFetch.post(
      routes.add(),
      JSON.stringify(test)
    ))
  }

  async update(test: Test): Promise<boolean> {
    //TODO validation
    return !!(await ProtectedFetch.put(
      routes.update(test.Id!),
      JSON.stringify(test)
    ))
  }

  async delete(id: number): Promise<boolean> {
    if (!!(await ProtectedFetch.delete(routes.delete(id)))) {
      this.NotificationService!.showSuccess('Тест успешно удален')
      return true
    }
    return false
  }

  async getAll(params: ITestFilter): Promise<IResult<IPagedData<Test>>> {
    return getResult(await ProtectedFetch.get(UrlBuilder.Build(routes.add(), params)))
  }

  async get(id: number): Promise<IResult<Test>> {
    return getResult(await ProtectedFetch.get(routes.get(id)))
  }
  
  async getByDisciplineId(disciplineId: number, options: IPagingOptions): Promise<IResult<IPagedData<Test>>> {
    return getResult(await ProtectedFetch.get(
      UrlBuilder.Build(routes.getByDisciplineId(disciplineId), options)
    ))
  }
}