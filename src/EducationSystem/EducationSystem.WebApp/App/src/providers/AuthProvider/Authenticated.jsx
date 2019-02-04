import React, {Component} from 'react'
import {withAuthenticated} from "./AuthProvider";

@withAuthenticated
class Authenticated extends Component {
  render() {
    const {children, auth: {checkAuth}, inRole: role} = this.props;
    return !!role ? checkAuth(role) && children : checkAuth() && children;
  }
}

export default Authenticated