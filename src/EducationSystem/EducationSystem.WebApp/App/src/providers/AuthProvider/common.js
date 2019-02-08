import {TOKEN} from './constants'
import Cookies from 'js-cookie'

const emailRegular = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/

const ValidateAuthModel = ({Email, Password}) => {
  if (typeof Email !== 'string')
    throw 'Почтовый адресс не корректный'
  if (typeof Password !== 'string' || Password.length < 6)
    throw 'Пароль не корректный'
}

const setAuthData = ({Token}) => {
  Cookies.set(TOKEN, Token)
}

const clearAuthData = () => {
  Cookies.remove(TOKEN)
}

const getAuthData = () => ({
  Token: Cookies.get(TOKEN)
})

const checkAuthData = ({Token}) =>
  !!Token

const capitalize = str => {
  str = str || ''
  return str.slice(0, 1).toUpperCase() + str.slice(1).toLowerCase()
}

const getInitials = str => !!str.length
  ? `${str.slice(0, 1)}.`
  : str

const getFullName = ({LastName, FirstName, MiddleName}, withInitials) => {
  LastName = capitalize(LastName || '')
  FirstName = capitalize(FirstName || '')
  MiddleName = capitalize(MiddleName || '')

  let fullName = `${LastName} `
  return fullName + (withInitials
      ? `${getInitials(FirstName)} ${getInitials(MiddleName)}`
      : `${FirstName} ${MiddleName}`
  ).trim()
}

export {
  ValidateAuthModel,
  setAuthData,
  clearAuthData,
  getAuthData,
  checkAuthData,
  getFullName
}