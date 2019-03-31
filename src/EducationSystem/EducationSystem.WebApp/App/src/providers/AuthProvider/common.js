import Cookies from 'js-cookie'
import {TokenCookieName} from '../../constants'
import {Exception} from '../../helpers'

export const ValidateAuthModel = ({Email, Password}) => {
  if (typeof Email !== 'string' || !Email.length)
    throw new Exception('Почтовый адрес некорректный')
  if (typeof Password !== 'string' || !Password.length)
    throw new Exception('Пароль некорректный')
}

export const handleAuthData = {
  get: () => ({Token: Cookies.get(TokenCookieName)}),
  set: ({Token}) => {
    if (!Token) throw new Exception('Token not found')
    Cookies.set(TokenCookieName, Token)
  },
  clear: () => Cookies.remove(TokenCookieName),
  check: ({Token} = {Token: Cookies.get(TokenCookieName)}) => !!Token
}
