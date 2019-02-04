import React, {Component} from 'react'
import {withAuthenticated} from "./AuthProvider";

@withAuthenticated
class NotAuthenticated extends Component {
  render() {
    return !this.props.auth.checkAuth() && this.props.children
  }
}

export default NotAuthenticated