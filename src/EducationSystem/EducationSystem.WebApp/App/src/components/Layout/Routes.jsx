import React from 'react'
import {Route, Switch} from 'react-router-dom'
import ProtectedRoute from './ProtectedRoute'
import Account from '../pages/Account/Account'
import SignIn from '../pages/SignIn/SignIn'
import Home from '../pages/Home/Home'
import Tests from '../pages/Tests/Tests'
import HandleTest from '../pages/HandleTest/HandleTest'
import ThemesPage from '../pages/Themes/ThemesPage'
import QuestionHandling from '../pages/QuestionHandling/QuestionHandling'

const Routes = () =>
  <Switch>
    <Route exact path='/' component={Home}/>
    <Route path='/signin' component={SignIn}/>
    
    <ProtectedRoute path='/account' component={Account}/>
    
    <ProtectedRoute path='/handletest' component={HandleTest} userRole='Admin'/>
    <ProtectedRoute path='/tests' component={Tests} userRole='Admin'/>
    <ProtectedRoute path='/themes' component={ThemesPage} userRole='Admin'/>
    <ProtectedRoute exact path='/question/:themeId' component={QuestionHandling} userRole='Admin'/>
  </Switch>

export default Routes