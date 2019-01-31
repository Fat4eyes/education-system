import {EMAIL, TOKEN, USER} from "./constants";
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

const setAuthData = ({Token, User, Email}) => {
  Cookies.set(TOKEN, Token);
  Cookies.set(USER, JSON.stringify(User));
  Cookies.set(EMAIL, Email);
};

const clearAuthData = () => {
  Cookies.remove(TOKEN);
  Cookies.remove(USER);
  Cookies.remove(EMAIL);
};

const getAuthData = () => ({
  Token: Cookies.get(TOKEN),
  User: Cookies.getJSON(USER),
  Email: Cookies.get(EMAIL)
});

const checkAuthData = ({Token, User, Email}) =>
  !!Token && !!User && !!Email;

export {
  ValidateAuthModel,
  setAuthData,
  clearAuthData,
  getAuthData,
  checkAuthData
}