import Model from './Model'
import {TUserRole} from '../common/enums'

type TRoles = {
  [propName in TUserRole]?: boolean
}

export default class User {
  FirstName: string
  MiddleName: string
  LastName: string
  Email: string
  Active: boolean
  Roles: TRoles = {}

  constructor(user: ApiUser) {
    this.FirstName = user.FirstName
    this.LastName = user.LastName
    this.MiddleName = user.MiddleName
    this.Email = user.Email
    this.Active = user.Active

    user.Roles.forEach(role => this.Roles[role.Name] = true)
  }
}

export class ApiUser extends Model {
  FirstName: string = ''
  MiddleName: string = ''
  LastName: string = ''
  Email: string = ''
  Active: boolean = false
  Roles: Array<{
    Name: TUserRole
    Id: number
  }> = []
}