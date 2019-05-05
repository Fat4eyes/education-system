import {IPagingOptions} from './PagedData'
import {TestType} from '../common/enums'

export default interface INameFilter {
  Name?: string 
}
 
export interface ITestFilter extends INameFilter, IPagingOptions {
  DisciplineId?: number
  OnlyActive?: boolean
  TestType?: TestType
}