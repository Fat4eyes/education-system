import IStudentService from '../abstractions/IStudentService'
import Discipline, {IFilterDiscipline, IOptionsDiscipline} from '../../models/Discipline'
import IPagedData from '../../models/PagedData'
import {Exception, ProtectedFetch, UrlBuilder} from '../../helpers'
import {studentRoutes} from '../../routes'
import TestData from '../../models/TestData'
import TestExecution from '../../models/TestExecution'

export default class StudentService implements IStudentService {
  async getDisciplines(filter?: IFilterDiscipline, options?: IOptionsDiscipline): Promise<IPagedData<Discipline> | Exception> {
    return await ProtectedFetch.get(UrlBuilder.Build(studentRoutes.getDisciplines(), {...options, ...filter}));
  }

  async getTestData(testId: number): Promise<TestData | Exception> {
    return await ProtectedFetch.get(UrlBuilder.Build(studentRoutes.getTestData(testId)))
  }

  async getTestExecution(testId: number): Promise<TestExecution | Exception> {
    return await ProtectedFetch.get(UrlBuilder.Build(studentRoutes.getTestExecution(testId)));
  }
}