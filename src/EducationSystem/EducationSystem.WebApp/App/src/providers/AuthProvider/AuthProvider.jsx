import React, {Component} from 'react'
import AuthContext from './AuthContext'
import Fetch from '../../helpers/Fetch'
import {checkAuthData, clearAuthData, getAuthData, getFullName, setAuthData, ValidateAuthModel} from './common'
import ProtectedFetch from '../../helpers/ProtectedFetch'
import {withSnackbar} from 'notistack'
import AuthModal from './AuthModal/AuthModal'
import {authRoutes, usersRoutes} from '../../routes'
import UrlBuilder from '../../helpers/UrlBuilder'

const defaultState = {
  Token: null,
  User: {},
  isAuthModalOpen: false
}

const {Provider, Consumer} = AuthContext

@withSnackbar
class AuthProvider extends Component {
  constructor(props) {
    super(props)

    const authData = getAuthData()

    if (checkAuthData(authData)) {
      this.state = {
        ...defaultState,
        ...authData
      }
    } else {
      clearAuthData()
      this.state = {...defaultState}
    }
  }

  handleError = e =>
    this.props.enqueueSnackbar(e, {
      variant: 'error',
      anchorOrigin: {
        vertical: 'bottom',
        horizontal: 'right'
      }
    })

  async componentDidMount() {
    if (!!this.state.Token) {
      let success = await ProtectedFetch.check(this.actions.signOut)
      if (success) {
        let url = UrlBuilder.Build(usersRoutes.getInfo, {
          WithRoles: true
        })

        const User = await ProtectedFetch.get(url, this.handleError)
        this.setState({User})
      }
    }
  }

  handleAuthModal = value => () => this.setState({isAuthModalOpen: value})

  actions = {
    signIn: async authModel => {
      try {
        ValidateAuthModel(authModel)
      } catch (e) {
        this.handleError(e)
        return false
      }

      const authData = await Fetch.post(authRoutes.signIn, JSON.stringify(authModel), this.handleError)

      if (authData) {
        setAuthData(authData)
        let url = UrlBuilder.Build(usersRoutes.getInfo, {
          WithRoles: true
        })

        const User = await ProtectedFetch.get(url, this.handleError)
        this.setState({...authData, User})
        return true
      }

      return false
    },
    signOut: () => {
      clearAuthData()
      this.setState({...defaultState})
    },
    checkAuth: (role) => {
      const isAuthenticated = checkAuthData(getAuthData())

      if (!!role) {
        console.log(this.state.User)
        
        return isAuthenticated && 
          this.state.User && 
          this.state.User.Roles &&
          this.state.User.Roles.find(r => r.Name === role)
      }

      return isAuthenticated
    },
    getFullName: withInitial => getFullName(this.state.User, withInitial),
    openAuthModal: this.handleAuthModal(true)
  }

  render() {
    return <Provider value={{...this.state, ...this.actions}}>
      {this.props.children}
      <AuthModal
        open={this.state.isAuthModalOpen}
        handleClose={this.handleAuthModal(false)}
        {...this.state}
        {...this.actions}/>
    </Provider>
  }
}

const withAuthenticated = Component => props =>
  <Consumer>
    {values => <Component {...props} auth={{...values}}/>}
  </Consumer>

export default AuthProvider

export {withAuthenticated}