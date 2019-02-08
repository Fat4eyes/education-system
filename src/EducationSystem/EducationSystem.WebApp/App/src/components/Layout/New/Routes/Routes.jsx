import React from 'react'
import {Route, Switch} from 'react-router-dom'
import ProtectedRoute from './ProtectedRoute'
import Account from '../../../Account/Account'
import Tests from '../../../Tests/Tests'
import SignIn from '../../../pages/SignIn/SignIn'
import Home from '../../../Home/Home'

const Routes = () =>
  <Switch>
    <Route exact path='/' component={Home}/>
    <Route exact path='/signin' component={SignIn}/>
    <ProtectedRoute path='/account' component={Account}/>
    <ProtectedRoute path='/tests' component={Tests}/>
  </Switch>

export default Routes