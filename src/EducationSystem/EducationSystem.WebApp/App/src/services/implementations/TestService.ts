import ITestService from '../abstractions/ITestService'
import Test from '../../models/Test'
import {ProtectedFetch, UrlBuilder} from '../../helpers'
import {testRoutes} from '../../routes'

export default class TestService implements ITestService {
  async add(test: Test) {
    return await ProtectedFetch.post(UrlBuilder.Build(testRoutes.add), JSON.stringify(test))
  }
}