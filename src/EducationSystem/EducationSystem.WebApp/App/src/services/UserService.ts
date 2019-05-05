import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'
import Fetch from '../helpers/Fetch'
import ProtectedFetch from '../helpers/ProtectedFetch'
import User from '../models/User'
import {getResult, IResult} from './IServices'

export default interface IUserService {
  getToken(userData: { Email: string, Password: string }): Promise<IResult<string>>
  checkToken(): Promise<boolean>
  getData(): Promise<IResult<User>>
}

export class UserService implements IUserService {
  @inject private NotificationService?: INotificationService

  async getToken({Email, Password}: ISignIn): Promise<IResult<string>> {
    if (!Email || !Password) {
      this.NotificationService!.showError('Проверьте правильность указанных данных')
      return getResult()
    }
    
    return getResult(await Fetch.post('api/token/generate', {Email, Password}))
  }

  async checkToken(): Promise<boolean> {
    return !!await ProtectedFetch.check()
  }

  async getData(): Promise<IResult<User>> {
    const apiUser = await ProtectedFetch.get('/api/users/current')
    return getResult(new User(apiUser))
  }
}

export interface ISignIn {
  Email: string,
  Password: string
}