import React, {Component} from 'react'
import {withAuthenticated} from "./AuthProvider";

@withAuthenticated
class NotAuthenticated extends Component {
  render() {
    return !this.props.auth.isAuthenticated() && this.props.children
  }
}

export default NotAuthenticated