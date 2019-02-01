import React, {Component} from 'react'
import {Route} from 'react-router-dom'
import {withAuthenticated} from '../../../../providers/AuthProvider/AuthProvider'
import SignIn from '../SignIn/SignIn';
import history from '../../../../helpers/history'

@withAuthenticated
class ProtectedRoute extends Component {
  render() {
    let {component: Component, auth: {isAuthenticated: checkAuth }, ...rest} = this.props;
    let isAuthenticated = checkAuth();

    const handlePrivateRender = props => <>
      {isAuthenticated && <Component {...props} />}
      <SignIn open={!isAuthenticated}
              handleReject={() => history.push('/')}/>
    </>;

    return <Route {...rest} render={handlePrivateRender}/>
  }
}

export default ProtectedRoute