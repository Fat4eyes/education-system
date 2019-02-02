import {TOKEN, USER} from "./constants";
import Cookies from 'js-cookie'

const emailRegular = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;

const ValidateAuthModel = ({Email, Password, Remember}) => {
  if (typeof Email !== 'string' || !emailRegular.test(Email))
    throw 'Почтовый адресс не корректный';
  if (typeof Password !== 'string' || Password.length < 6)
    throw 'Пароль не корректный';
  if (typeof Remember !== 'boolean')
    throw 'Запомнить не заданно';
};

const setAuthData = ({token, user}) => {
  Cookies.set(TOKEN, token);
  Cookies.set(USER, JSON.stringify(user));
};

const clearAuthData = () => {
  Cookies.remove(TOKEN);
  Cookies.remove(USER);
};

const getAuthData = () => ({
  token: Cookies.get(TOKEN),
  user: Cookies.getJSON(USER)
});

const checkAuthData = ({token, user}) =>
  !!token && !!user;

const capitalize = str => {
  str = str || '';
  return str.slice(0, 1).toUpperCase() + str.slice(1).toLowerCase()
};

const getInitials = str => !!str.length
  ? `${str.slice(0, 1)}.`
  : str;

const getFullName = ({lastName, firstName, middleName}) => {
  lastName = capitalize(lastName || '');
  firstName = capitalize(firstName || '');
  middleName = capitalize(middleName || '');
  
  return `${lastName} ${getInitials(firstName)} ${getInitials(middleName)}`.trim()
};

export {
  ValidateAuthModel,
  setAuthData,
  clearAuthData,
  getAuthData,
  checkAuthData,
  getFullName
}