import Exception from '../../helpers/Exception'
import IPagedData from '../../models/PagedData'
import Discipline, {IFilterDiscipline, IOptionsDiscipline} from '../../models/Discipline'
import TestData from '../../models/TestData'
import TestExecution from '../../models/TestExecution'

export default interface IStudentService {
  getDisciplines(filter?: IFilterDiscipline, options?: IOptionsDiscipline): Promise<IPagedData<Discipline> | Exception>
  getTestData(testId: number): Promise<TestData | Exception>
  getTestExecution(testId: number): Promise<TestExecution | Exception>
}