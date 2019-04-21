import IDisciplineService from '../abstractions/IDisciplineService'
import {Exception, ProtectedFetch, UrlBuilder} from '../../helpers'
import {disciplineRoutes} from '../../routes'
import {IPagingOptions} from '../../models/PagedData'
import INameFilter from '../../models/Filters'
import Theme from '../../models/Theme'

export default class DisciplineService implements IDisciplineService {
  async getAll(options?: IPagingOptions, filter?: INameFilter) {
    return await ProtectedFetch.get(UrlBuilder.Build(disciplineRoutes.getDisciplines, {...options, ...filter}))
  }
  
  async getThemes(id: number, options?: IPagingOptions) {
    return await ProtectedFetch.get(UrlBuilder.Build(disciplineRoutes.getDisciplineThemes(id), options || {
      All: true
    }))
  }

  async updateDisciplineThemes(id: number, themes: Array<Theme>): Promise<void | Exception> {
    return await ProtectedFetch.put(UrlBuilder.Build(disciplineRoutes.updateDisciplineThemes(id)), JSON.stringify(
      themes));
  }
}