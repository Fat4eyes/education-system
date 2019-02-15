import React, {Component} from 'react'
import Fetch from '../../helpers/Fetch'
import {handleAuthData, ValidateAuthModel} from './common'
import ProtectedFetch from '../../helpers/ProtectedFetch'
import {withSnackbar} from 'notistack'
import {authRoutes, usersRoutes} from '../../routes'
import UrlBuilder from '../../helpers/UrlBuilder'
import Snackbar from '../../helpers/Snackbar'

const {Provider, Consumer} = React.createContext()

const defaultState = {Token: null, User: {}}

class IsAuthenticated {
  constructor(byToken, byRole = null) {
    this.byToken = byToken
    this.byRole = byRole
  }

  result() {
    return !!this.byToken && (this.byRole === null || this.byRole)
  }
}

@withSnackbar
class AuthProvider extends Component {
  constructor(props) {
    super(props)

    const authData = handleAuthData.get()

    if (handleAuthData.check(authData)) {
      this.state = {
        ...defaultState,
        ...authData
      }
    } else {
      handleAuthData.clear()
      this.state = {...defaultState}
    }

    this.Snackbar = new Snackbar(this.props.enqueueSnackbar)
  }

  getUserData = async () => await ProtectedFetch.get(
    UrlBuilder.Build(usersRoutes.getInfo, {WithRoles: true}),
    this.Snackbar.Error
  )

  async componentDidMount() {
    if (this.state.Token && await ProtectedFetch.check(this.actions.signOut)) {
      this.setState({User: await this.getUserData()})
    }
  }

  actions = {
    signIn: async authModel => {
      try {
        ValidateAuthModel(authModel)

        const authData = await Fetch.post(authRoutes.signIn, authModel, this.Snackbar.Error)

        if (authData) {
          handleAuthData.set(authData)

          this.setState({...authData, User: await this.getUserData()})

          return true
        }
      } catch (e) {
        this.Snackbar.Error(e)

        handleAuthData.clear()
        this.setState({...defaultState})
      }
    },
    signOut: () => {
      handleAuthData.clear()
      return this.setState({...defaultState})
    },
    checkAuth: role => {
      if (!role) return new IsAuthenticated(handleAuthData.check()).result()

      return new IsAuthenticated(handleAuthData.check(),
        this.state.User && this.state.User.Roles && !!this.state.User.Roles.find(r => r.Name === role)
      ).result()
    }
  }

  render() {
    return <Provider value={{...this.state, ...this.actions}}>
      {this.props.children}
    </Provider>
  }
}

export default AuthProvider

export const withAuthenticated = Component => props =>
  <Consumer>
    {values => <Component {...props} auth={{...values}}/>}
  </Consumer>