import React from 'react'
import {Route, Switch} from 'react-router-dom'
import ProtectedRoute from './ProtectedRoute'
import Account from '../../../Account/Account'
import SignIn from '../../../pages/SignIn/SignIn'
import Home from '../../../Home/Home'
import Tests from '../../../pages/Tests/Tests'

const Routes = () =>
  <Switch>
    <Route exact path='/' component={Home}/>
    <Route path='/signin' component={SignIn}/>
    <ProtectedRoute path='/account' component={Account}/>
    <ProtectedRoute path='/tests' component={Tests}/>
  </Switch>

export default Routes