import React from 'react'
import {Route, Switch} from 'react-router-dom'
import Account from '../pages/Account/Account'
import SignIn from '../pages/SignIn/SignIn'
import Home from '../pages/Home/Home'
import Tests from '../pages/Tests/Tests'
import HandleTest from '../pages/HandleTest/HandleTest'
import ThemesPage from '../pages/Themes/ThemesPage'
import QuestionHandling from '../pages/QuestionHandling/QuestionHandling'
import MaterialHandling from '../pages/Material/MaterialHandling'
import RouteProxy from './RouteProxy'

const Routes = () =>
  <Switch>
    <Route exact path='/' component={Home}/>
    <Route exact path='/signin' component={SignIn}/>
    <RouteProxy path='/account' component={Account} roles={['Admin', 'Lecturer', 'Student']} title='Профиль'/>
    <RouteProxy exact key={'create'} path='/createtest' component={HandleTest} roles={['Admin', 'Lecturer']} title='Создание теста'/>
    <RouteProxy exact key={'edit'} path='/edittest/:id' component={HandleTest} roles={['Admin', 'Lecturer']} title='Редактирование теста'/>
    <RouteProxy path='/tests' component={Tests} roles={['Admin', 'Lecturer']} title='Тесты'/>
    <RouteProxy path='/themes' component={ThemesPage} roles={['Admin', 'Lecturer']} title='Темы'/>
    <RouteProxy exact path='/question/:themeId/:id' component={QuestionHandling} roles={['Admin', 'Lecturer']} title='Редактирование вопроса'/>
    <RouteProxy exact path='/question/:themeId' component={QuestionHandling} roles={['Admin', 'Lecturer']} title='Создание вопроса'/>
    <RouteProxy path='/materials/:id' component={MaterialHandling} roles={['Admin', 'Lecturer']} title='Редактирование материала'/>
    <RouteProxy path='/materials' component={MaterialHandling} roles={['Admin', 'Lecturer']} title='Создание материала'/>
  </Switch>

export default Routes