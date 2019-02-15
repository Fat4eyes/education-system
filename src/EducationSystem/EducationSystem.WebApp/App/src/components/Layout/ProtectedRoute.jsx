import React, {Component} from 'react'
import {Redirect, Route} from 'react-router-dom'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider'
import {withSnackbar} from 'notistack'
import Snackbar from '../../helpers/Snackbar'

@withSnackbar
@withAuthenticated
class ProtectedRoute extends Component {
  constructor(props) {
    super(props)

    let {auth, role} = this.props
    
    this.state = {
      IsAuthenticated: auth.checkAuth(role)
    }

    this.Snackbar = new Snackbar(this.props.enqueueSnackbar)
  }
  
  componentDidMount() {
    let {role} = this.props
    
    if (role && !this.state.IsAuthenticated) {
      this.Snackbar.Error('Недостаточно прав')
    }
  }
  
  render() {
    let {component: Component, role, auth, ...rest} = this.props

    const handlePrivateRender = props => auth.checkAuth(role)
      ? <Component {...props} />
      : <Redirect to={{pathname: role ? '/' : '/signin', state: {from: props.location}}}/>

    return <Route {...rest} render={handlePrivateRender}/>
  }
}

export default ProtectedRoute