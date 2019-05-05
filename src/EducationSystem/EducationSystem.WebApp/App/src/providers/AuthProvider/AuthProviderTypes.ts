import User from '../../models/User'
import {TUserRole} from '../../common/enums'

export interface IAuthAction {
  Token?: string
  User?: User
}

export interface IAuthFields {
  signIn: (authModel: {
    Email: string,
    Password: string
  }) => void
  signOut: () => void
  checkAuth: (roles?: TUserRole | Array<TUserRole>) => boolean
}

export type TAuthProps = {
  auth: IAuthAction & IAuthFields
}