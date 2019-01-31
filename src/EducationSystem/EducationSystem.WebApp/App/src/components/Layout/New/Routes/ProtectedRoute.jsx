import React, {Component} from 'react'
import {Route} from 'react-router-dom'
import {withAuthenticated} from '../../../../providers/AuthProvider/AuthProvider'
import SignIn from '../SignIn/SignIn';
import history from '../../../../helpers/history'

@withAuthenticated
class ProtectedRoute extends Component {
  state = {
    singInModalOpen: true
  };

  handleSingInModal = () => {
    this.setState({singInModalOpen: false});
  };

  render() {
    let {component: Component, auth: {isAuthenticated}, ...rest} = this.props;

    const handlePrivateRender = props => isAuthenticated()
      ? <Component {...props} />
      : <SignIn open={true} handleClose={this.handleSingInModal} handleReject={() => history.push('/')}/>;

    return <Route {...rest} render={handlePrivateRender}/>
  }
}

export default ProtectedRoute