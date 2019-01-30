import React, {Component} from 'react'
import {Route} from 'react-router-dom'
import {AuthenticateConsumer} from '../../../../services/authService'
import SignIn from '../SignIn/SignIn';
import history from '../../../../helpers/history'

class ProtectedRoute extends Component {
  state = {
    singInModalOpen: true
  };

  handleSingInModal = () => {
    this.setState({singInModalOpen: false});
  };

  render() {
    let {component: Component, ...rest} = this.props;

    const handlePrivateRender = props => <AuthenticateConsumer>
      {({isAuthenticated}) => isAuthenticated()
        ? <Component {...props} />
        : <SignIn open={true} handleClose={this.handleSingInModal} handleReject={() => history.push('/')}/>}
    </AuthenticateConsumer>;

    return <Route
      {...rest}
      render={handlePrivateRender}
    />
  }
}

export default ProtectedRoute