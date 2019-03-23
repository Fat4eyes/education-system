import IDisciplineService from '../abstractions/IDisciplineService'
import {ProtectedFetch, UrlBuilder} from '../../helpers'
import {disciplineRoutes} from '../../routes'
import {IPagingOptions} from '../../models/PagedData'

export default class DisciplineService implements IDisciplineService {
  async getAll(options?: IPagingOptions) {
    return await ProtectedFetch.get(UrlBuilder.Build(disciplineRoutes.getDisciplines, options));
  }
  
  async getThemes(id: number) {
    return await ProtectedFetch.get(UrlBuilder.Build(disciplineRoutes.getDisciplineThemes(id), {
      All: true
    }))
  }
}