import React from 'react'
import {Route, Switch} from 'react-router-dom'
import Home from '../../../Home/Home'
import ProtectedRoute from './ProtectedRoute'
import Account from '../../../Account/Account';

const Routes = () =>
  <Switch>
    <Route exact path='/' component={Home}/>
    <ProtectedRoute path='/account' component={Account}/>
  </Switch>;

export default Routes