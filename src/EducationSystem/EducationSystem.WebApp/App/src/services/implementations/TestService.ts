import ITestService from '../abstractions/ITestService'
import Test from '../../models/Test'
import {ProtectedFetch, UrlBuilder} from '../../helpers'
import {testRoutes} from '../../routes'

export default class TestService implements ITestService {
  constructor() {
    
  }
  
  getAll(): Test[] {
    let test = new Test()
    
    return [test]
  }

  async get(id: number): Promise<Test> {
    return await ProtectedFetch.get()
  }

  add(test: Test): boolean {
    return true;
  }
  
  test(): number {
    return 12
  }
}