import IPagedData, {IPagingOptions} from '../models/PagedData'
import INameFilter from '../models/Filters'
import Discipline from '../models/Discipline'
import ProtectedFetch from '../helpers/ProtectedFetch'
import UrlBuilder from '../helpers/UrlBuilder'
import {getResult, IResult} from './IServices'

export default interface IDisciplineService {
  getAll(options?: IPagingOptions, filter?: INameFilter): Promise<IResult<IPagedData<Discipline>>>
  get(id: number): Promise<IResult<Discipline>>
}

export class DisciplineService implements IDisciplineService {
  async getAll(options?: IPagingOptions, filter?: INameFilter): Promise<IResult<IPagedData<Discipline>>> {
    let result = await ProtectedFetch.get(UrlBuilder.Build(`/api/disciplines`, {...options, ...filter}))

    return getResult(result)
  }

  async get(id: number): Promise<IResult<Discipline>> {
    return getResult(await ProtectedFetch.get(`/api/disciplines/${id}`))
  }
}