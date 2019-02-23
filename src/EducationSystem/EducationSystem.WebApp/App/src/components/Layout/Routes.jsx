import React from 'react'
import {Route, Switch} from 'react-router-dom'
import ProtectedRoute from './ProtectedRoute'
import Account from '../pages/Account/Account'
import SignIn from '../pages/SignIn/SignIn'
import Home from '../pages/Home/Home'
import Tests from '../pages/Tests/Tests'
import HandleTest from '../pages/HandleTest/HandleTest'

const Routes = () =>
  <Switch>
    <Route exact path='/' component={Home}/>
    <Route path='/signin' component={SignIn}/>
    <Route path='/handletest' component={HandleTest}/>
    <ProtectedRoute path='/account' component={Account}/>
    <ProtectedRoute path='/tests' component={Tests} userRole='Admin'/>
  </Switch>

export default Routes