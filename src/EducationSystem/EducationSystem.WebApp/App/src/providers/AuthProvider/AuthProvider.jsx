import React, {Component} from 'react'
import {handleAuthData, ValidateAuthModel} from './common'
import {withSnackbar} from 'notistack'
import Snackbar from '../../helpers/Snackbar'
import {inject} from '../../infrastructure/di/inject'

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
  @inject UserService

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

  async componentDidMount() {
    if (this.state.Token && await this.UserService.checkToken()) {
      this.setState({User: (await this.UserService.getData()).data})
    } else {
      this.actions.signOut()
    }
  }

  actions = {
    signIn: async authModel => {
      try {
        ValidateAuthModel(authModel)

        const result = await this.UserService.getToken(authModel)

        if (result.success) {
          handleAuthData.set(result.data)

          this.setState({...result.data, User: (await this.UserService.getData()).data})

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

      const result = this.state.User && this.state.User.Roles

      if (typeof role === 'object') {
        return new IsAuthenticated(handleAuthData.check(),
          result && !!role.find(r => this.state.User.Roles[r])
        ).result()
      }

      return new IsAuthenticated(handleAuthData.check(),
        result && !!this.state.User.Roles[role]
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