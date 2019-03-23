import Test from '../../models/Test'
import Exception from '../../helpers/Exception'

export default interface ITestService {
  add(test: Test): Promise<Test | Exception>,
}