import React, {Component} from 'react'
import AuthContext from './AuthContext'
import {baseFetch as fetch, protectedFetch} from '../../helpers/fetch';
import {checkAuthData, clearAuthData, getAuthData, setAuthData, ValidateAuthModel} from './common';

const defaultState = {
  Token: null,
  User: {},
  Email: null
};

const {Provider, Consumer} = AuthContext;

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
        return isAuthenticated && authData.User.Roles.find(r => r.Name === role)
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