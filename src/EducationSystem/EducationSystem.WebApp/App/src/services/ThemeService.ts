import Theme from '../models/Theme'
import {getResult, IResult} from './IServices'
import ProtectedFetch from '../helpers/ProtectedFetch'
import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'
import IPagedData, {IPagingOptions} from '../models/PagedData'
import UrlBuilder from '../helpers/UrlBuilder'
const routes = {
  add: () => '/api/themes',
  delete: (id: number) => `/api/themes/${id}`,
  update: (id: number) => `/api/themes/${id}`,
  getByDisciplineId: (id: number) => `/api/disciplines/${id}/themes`,
  getByTestId: (id: number) => `/api/tests/${id}/themes`,
  setOrder: (id: number) => `/api/disciplines/${id}/themes`
}

export default interface IThemeService {
  add(theme: Theme): Promise<IResult<number>>
  delete(id: number): Promise<boolean>
  update(theme: Theme): Promise<boolean>
  getByDisciplineId(disciplineId: number, options: IPagingOptions): Promise<IResult<IPagedData<Theme>>>
  getByTestId(testId: number, options: IPagingOptions): Promise<IResult<IPagedData<Theme>>>
  setOrder(disciplineId: number, themes: Array<Theme>): Promise<boolean>
}

export class ThemeService implements IThemeService {
  @inject private NotificationService?: INotificationService

  async add(theme: Theme): Promise<IResult<number>> {
    if (!this.validateTheme(theme)) return getResult()

    return getResult(await ProtectedFetch.post(
      routes.add(),
      JSON.stringify(theme)
    ))
  }

  async update(theme: Theme): Promise<boolean> {
    if (!this.validateTheme(theme)) return false
    
    return !!(await ProtectedFetch.put(
      routes.update(theme.Id!),
      JSON.stringify(theme)
    ))
  }

  async delete(id: number): Promise<boolean> {
    return !!(await ProtectedFetch.delete(routes.delete(id)))
  }

  async getByDisciplineId(disciplineId: number, options: IPagingOptions): Promise<IResult<IPagedData<Theme>>> {
    return getResult(await ProtectedFetch.get(
      UrlBuilder.Build(routes.getByDisciplineId(disciplineId), options)
    ))
  }

  async getByTestId(testId: number, options: IPagingOptions): Promise<IResult<IPagedData<Theme>>> {
    return getResult(await ProtectedFetch.get(
      UrlBuilder.Build(routes.getByTestId(testId), options)
    ));
  }

  async setOrder(disciplineId: number, themes: Array<Theme>): Promise<boolean> {
    return await ProtectedFetch.put(
      routes.setOrder(disciplineId), 
      JSON.stringify(themes)
    )
  }
  
  private validateTheme(theme: Theme): boolean {
    if (!theme.Name) {
      this.NotificationService!.showError('Не указано название темы')
      return false
    }

    return true
  }
}