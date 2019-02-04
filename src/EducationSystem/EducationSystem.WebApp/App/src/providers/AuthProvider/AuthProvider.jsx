import React, {Component} from 'react'
import AuthContext from './AuthContext'
import Fetch from '../../helpers/Fetch';
import {checkAuthData, clearAuthData, getAuthData, setAuthData, ValidateAuthModel, getFullName} from './common';
import ProtectedFetch from '../../helpers/ProtectedFetch';
import {withSnackbar} from "notistack";
import {
  Avatar, Button, Checkbox,
  CircularProgress,
  FormControl,
  FormControlLabel,
  Input,
  InputLabel,
  Modal,
  Typography
} from "@material-ui/core";
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import AuthModal from "./AuthModal/AuthModal";

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

  async componentDidMount() {
    if (!!this.state.token) {
      await ProtectedFetch.check(this.actions.signOut)
    }
  }

  handleAuthModal = value => () => this.setState({isAuthModalOpen: value});
  
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
        this.setState({...authData});
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
    getFullName: () => getFullName(this.state.user),
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