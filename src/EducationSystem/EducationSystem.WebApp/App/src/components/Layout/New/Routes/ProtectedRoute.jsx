import React, {Component} from 'react'
import {Redirect, Route} from 'react-router-dom'
import {withAuthenticated} from '../../../../providers/AuthProvider/AuthProvider'

@withAuthenticated
class ProtectedRoute extends Component {
  render() {
    let {component: Component, auth, ...rest} = this.props;
    let isAuthenticated = auth.checkAuth();

    const handlePrivateRender = props => isAuthenticated 
      ? <Component {...props} />
      : <Redirect to={{pathname: "/signin", state: { from: props.location }}}/>;

    return <Route {...rest} render={handlePrivateRender}/>
  }
}

export default ProtectedRoute