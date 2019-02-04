import React, {Component} from 'react'
import AuthContext from './AuthContext'
import Fetch from '../../helpers/Fetch';
import {checkAuthData, clearAuthData, getAuthData, setAuthData, ValidateAuthModel, getFullName} from './common';
import ProtectedFetch from '../../helpers/ProtectedFetch';
import {withSnackbar} from "notistack";
import AuthModal from './AuthModal/AuthModal';
import {authRoutes, usersReutes} from '../../routes';

const defaultState = {
  token: null,
  user: {},
  isAuthModalOpen: false
};

const {Provider, Consumer} = AuthContext;

@withSnackbar
class AuthProvider extends Component {
  constructor(props) {
    super(props);

    const authData = getAuthData();

    if (checkAuthData(authData)) {
      this.state = {
        ...defaultState,
        ...authData
      }
    } else {
      clearAuthData();
      this.state = {...defaultState}
    }
  }

  handleError = e =>
    this.props.enqueueSnackbar(e, {
      variant: 'error',
      anchorOrigin: {
        vertical: 'bottom',
        horizontal: 'right',
      },
    });
  
  async componentDidMount() {
    if (!!this.state.token) {
      let success =  await ProtectedFetch.check(this.actions.signOut);
      if (success) {
        const user = await ProtectedFetch.get(usersReutes.getInfo, this.handleError);
        this.setState({user});
      }
    }
  }

  handleAuthModal = value => () => this.setState({isAuthModalOpen: value});
  
  actions = {
    signIn: async authModel => {
      try {
        ValidateAuthModel(authModel)
      } catch (e) {
        this.handleError(e)
      }

      const authData = await Fetch.post(authRoutes.signIn, JSON.stringify(authModel), this.handleError);

      if (authData) {
        setAuthData(authData);
        const user = await ProtectedFetch.get(usersReutes.getInfo, this.handleError);
        this.setState({...authData, user});
        return true;
      }

      return false;
    },
    signOut: () => {
      clearAuthData();
      this.setState({...defaultState})
    },
    checkAuth: (role) => {
      const authData = getAuthData();
      const isAuthenticated = checkAuthData(authData);

      if (!!role) {
        return isAuthenticated && authData.user.roles.find(r => r.name === role)
      }

      return isAuthenticated;
    },
    getFullName: withInitial => getFullName(this.state.user, withInitial),
    openAuthModal: this.handleAuthModal(true)
  };

  render() {
    return <Provider value={{...this.state, ...this.actions}}>
      {this.props.children}
      <AuthModal 
        open={this.state.isAuthModalOpen}
        handleClose={this.handleAuthModal(false)}
        {...this.state} 
        {...this.actions}/>
    </Provider>;
  }
}

const withAuthenticated = Component => props =>
  <Consumer>
    {values => <Component {...props} auth={{...values}}/>}
  </Consumer>;

export default AuthProvider

export {withAuthenticated}