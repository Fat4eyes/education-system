import React from 'react'
import {Route, Switch} from 'react-router-dom'
import Home from '../../../Home/Home'
import ProtectedRoute from './ProtectedRoute'

const Routes = () =>
  <Switch>
    <Route exact path='/' component={Home}/>
    <ProtectedRoute path='/private' component={() => <div>Private</div>}/>
  </Switch>;

export default Routes