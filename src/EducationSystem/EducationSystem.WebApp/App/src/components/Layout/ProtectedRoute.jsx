import React, {Component} from 'react'
import {Redirect, Route} from 'react-router-dom'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider'
import {withSnackbar} from 'notistack'
import Snackbar from '../../helpers/Snackbar'
import {routes} from './Routes'

@withSnackbar
@withAuthenticated
class ProtectedRoute extends Component {
  constructor(props) {
    super(props)

    let {auth, userRole} = this.props
    
    this.state = {
      IsAuthenticated: auth.checkAuth(userRole)
    }

    this.Snackbar = new Snackbar(this.props.enqueueSnackbar)
  }
  
  componentDidMount() {
    let {userRole} = this.props
    
    if (userRole && !this.state.IsAuthenticated) {
      this.Snackbar.Error('Недостаточно прав')
    }
  }
  
  render() {
    let {component: Component, userRole, auth, ...rest} = this.props

    const handlePrivateRender = props => auth.checkAuth(userRole)
      ? <Component {...props} />
      : <Redirect to={{pathname: userRole ? routes.home : routes.signIn, state: {from: props.location}}}/>

    return <Route {...rest} render={handlePrivateRender}/>
  }
}

export default ProtectedRoute