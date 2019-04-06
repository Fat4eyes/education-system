import {Exception, ProtectedFetch, UrlBuilder} from '../../helpers'
import {materialRoutes} from '../../routes'
import IMaterialService from '../abstractions/IMaterialService'
import Material from '../../models/Material'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import INameFilter from '../../models/Filters'

export default class MaterialService implements IMaterialService {
  async add(material: Material): Promise<Material | Exception> {
    return await ProtectedFetch.post(UrlBuilder.Build(materialRoutes.add()), JSON.stringify(material))
  }

  async update(material: Material): Promise<Material | Exception> {
    return await ProtectedFetch.put(UrlBuilder.Build(materialRoutes.update(material.Id!)), JSON.stringify(material))
  }

  async get(id: number): Promise<Material | Exception> {
    return await ProtectedFetch.get(UrlBuilder.Build(materialRoutes.get(id)))
  }

  async getAll(options?: IPagingOptions, filter?: INameFilter): Promise<IPagedData<Material> | Exception> {
    return await ProtectedFetch.get(UrlBuilder.Build(materialRoutes.getAll(), {...options, ...filter}));
  }
}