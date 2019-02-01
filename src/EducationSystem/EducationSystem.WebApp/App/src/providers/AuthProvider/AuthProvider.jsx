import React, {Component} from 'react'
import AuthContext from './AuthContext'
import Fetch from '../../helpers/Fetch';
import {checkAuthData, clearAuthData, getAuthData, setAuthData, ValidateAuthModel} from './common';
import ProtectedFetch from '../../helpers/ProtectedFetch';
import {withSnackbar} from "notistack";

const defaultState = {
  token: null,
  user: {}
};

const {Provider, Consumer} = AuthContext;

@withSnackbar
class AuthProvider extends Component {
  constructor(props) {
    super(props);

    const authData = getAuthData();

    if (checkAuthData(authData)) {
      this.state = {...authData}
    } else {
      clearAuthData();
      this.state = {...defaultState}
    }
  }

  async componentDidMount() {
    if (!!this.state.token) {
      await ProtectedFetch.check(this.actions.signOut)
    }
  }

  actions = {
    signIn: async authModel => {
      const handleError = e =>
        this.props.enqueueSnackbar(e, {
          variant: 'error',
          anchorOrigin: {
            vertical: 'bottom',
            horizontal: 'right',
          },
        });

      try {
        ValidateAuthModel(authModel)
      } catch (e) {
        handleError(e)
      }

      const authData = await Fetch.post('/api/auth/signin', JSON.stringify(authModel), handleError);

      if (authData) {
        setAuthData(authData);
        this.setState(authData);
        return true;
      }

      return false;
    },
    signOut: () => {
      clearAuthData();
      this.setState({...defaultState})
    },
    isAuthenticated: (role) => {
      const authData = getAuthData();
      const isAuthenticated = checkAuthData(authData);

      if (!!role) {
        return isAuthenticated && authData.user.roles.find(r => r.name === role)
      }

      return isAuthenticated;
    }
  };

  render = () => <Provider value={{...this.state, ...this.actions}}>
    {this.props.children}
  </Provider>
}

const withAuthenticated = Component => props =>
  <Consumer>
    {values => <Component {...props} auth={{...values}}/>}
  </Consumer>;

export default AuthProvider

export {withAuthenticated}