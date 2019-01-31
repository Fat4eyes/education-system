import React, {Component} from 'react'
import {withAuthenticated} from "./AuthProvider";

@withAuthenticated
class Authenticated extends Component {
  render() {
    const {children, auth: {isAuthenticated}, inRole: role} = this.props;
    return !!role ? isAuthenticated(role) && children : isAuthenticated() && children;
  }
}

export default Authenticated