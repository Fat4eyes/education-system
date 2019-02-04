import React, {Component} from 'react'
import {Route} from 'react-router-dom'
import {withAuthenticated} from '../../../../providers/AuthProvider/AuthProvider'
import history from '../../../../helpers/history'
import AuthModal from "../../../../providers/AuthProvider/AuthModal/AuthModal";

@withAuthenticated
class ProtectedRoute extends Component {
  render() {
    let {component: Component, auth, ...rest} = this.props;
    let isAuthenticated = auth.checkAuth();

    const handlePrivateRender = props => <>
      {isAuthenticated && <Component {...props} />}
      <AuthModal open={!isAuthenticated} handleReject={() => history.push('/')} {...auth}/>
    </>;

    return <Route {...rest} render={handlePrivateRender}/>
  }
}

export default ProtectedRoute