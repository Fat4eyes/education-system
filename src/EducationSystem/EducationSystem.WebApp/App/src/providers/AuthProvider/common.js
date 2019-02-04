import {TOKEN, USER} from "./constants";
import Cookies from 'js-cookie'

const emailRegular = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;

const ValidateAuthModel = ({Email, Password}) => {
  if (typeof Email !== 'string')
    throw 'Почтовый адресс не корректный';
  if (typeof Password !== 'string' || Password.length < 6)
    throw 'Пароль не корректный';
};

const setAuthData = ({token}) => {
  Cookies.set(TOKEN, token);
};

const clearAuthData = () => {
  Cookies.remove(TOKEN);
};

const getAuthData = () => ({
  token: Cookies.get(TOKEN),
});

const checkAuthData = ({token}) =>
  !!token;

const capitalize = str => {
  str = str || '';
  return str.slice(0, 1).toUpperCase() + str.slice(1).toLowerCase()
};

const getInitials = str => !!str.length
  ? `${str.slice(0, 1)}.`
  : str;

const getFullName = ({lastName, firstName, middleName}, withInitials) => {
  lastName = capitalize(lastName || '');
  firstName = capitalize(firstName || '');
  middleName = capitalize(middleName || '');
  
  let fullName = `${lastName} `;
  return fullName + (withInitials 
    ? `${getInitials(firstName)} ${getInitials(middleName)}`
    : `${firstName} ${middleName}`
  ).trim()
};

export {
  ValidateAuthModel,
  setAuthData,
  clearAuthData,
  getAuthData,
  checkAuthData,
  getFullName
}