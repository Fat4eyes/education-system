import Material from '../models/Material'
import IPagedData, {IPagingOptions} from '../models/PagedData'
import INameFilter from '../models/Filters'
import {getResult, IResult} from './IServices'
import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'
import ProtectedFetch from '../helpers/ProtectedFetch'
import UrlBuilder from '../helpers/UrlBuilder'

export default interface IMaterialService {
  add(material: Material): Promise<IResult<number>>
  update(material: Material): Promise<boolean>
  get(id: number): Promise<IResult<Material>>
  getAll(options?: IPagingOptions, filter?: INameFilter): Promise<IResult<IPagedData<Material>>>
  delete(id: number): Promise<boolean>
}

export class MaterialService implements IMaterialService {
  @inject private NotificationService?: INotificationService

  async add(material: Material): Promise<IResult<number>> {
    if (!this.validateMaterial(material)) return getResult()

    return getResult(await ProtectedFetch.post('/api/materials', JSON.stringify(material)))
  }

  async get(id: number): Promise<IResult<Material>> {
    return getResult(await ProtectedFetch.get(`/api/materials/${id}`))
  }

  async getAll(options?: IPagingOptions, filter?: INameFilter): Promise<IResult<IPagedData<Material>>> {
    let result = await ProtectedFetch.get(UrlBuilder.Build(`/api/materials`, {...options, ...filter}))

    return getResult(result)
  }

  async delete(id: number): Promise<boolean> {
    if (!!(await ProtectedFetch.delete(`/api/materials/${id}`))) {
      this.NotificationService!.showSuccess('Материал успешно удален')
      return true
    }
    return false
  }

  async update(material: Material): Promise<boolean> {
    if (!this.validateMaterial(material)) return false
    return null !== (await ProtectedFetch.put(`/api/materials/${material.Id}`, JSON.stringify(material)))
  }

  private validateMaterial(material: Material): boolean {
    if (!material.Name) {
      this.NotificationService!.showError('Не указано название материала')
      return false
    }
    if (!material.Template) {
      this.NotificationService!.showError('Не указан шаблон материала')
      return false
    }

    return true
  }
}