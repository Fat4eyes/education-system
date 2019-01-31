import React, {Component} from 'react'
import AuthContext from './AuthContext'
import {baseFetch as fetch, protectedFetch} from '../../helpers/fetch';
import {checkAuthData, clearAuthData, getAuthData, setAuthData, ValidateAuthModel} from './common';

const defaultState = {
  Token: null,
  User: {},
  Email: null
};

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
    if (!!this.state.Token) {
      await protectedFetch.check(this.actions.signOut)
    }
  }

  actions = {
    signIn: async authModel => {
      ValidateAuthModel(authModel);

      const authData = await fetch.post('/api/auth/signin', JSON.stringify(authModel));

      setAuthData(authData);
      this.setState(authData)
    },
    signOut: () => {
      clearAuthData();
      this.setState({...defaultState})
    },
    isAuthenticated: (role) => {
      const authData = getAuthData();
      const isAuthenticated = checkAuthData(authData);

      if (!!role) {
        return isAuthenticated && authData.User.Roles.find(r => r.Name === role)
      }

      return isAuthenticated;
    }
  };

  render() {
    return <AuthContext.Provider value={{...this.state, ...this.actions}}>
      {this.props.children}
    </AuthContext.Provider>
  }
}

const withAuthenticated = Component =>
  props =>
    <AuthContext.Consumer>
      {values => <Component {...props} auth={{...values}}/>}
    </AuthContext.Consumer>;

@withAuthenticated
class Authenticated extends Component {
  render() {
    const {children, auth: {isAuthenticated}, inRole: role} = this.props;

    return !!role
      ? isAuthenticated(role) && children
      : isAuthenticated() && children;
  }
}

@withAuthenticated
class UnAuthenticated extends Component {
  render() {
    return !this.props.auth.isAuthenticated() && this.props.children
  }
}

export default AuthProvider

export {
  withAuthenticated,
  Authenticated,
  UnAuthenticated
}