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
import TestSelect from '../pages/student/tests/TestSelect'
import Test from '../pages/student/test/Test'

export const routes = {
    home: 
      '/',
    signIn: 
      '/signin',
    account: 
      '/account',
    tests: 
      '/tests',
    createTest: 
      '/tests/create',
    editTest: (id: string|number = ':id') => 
      `/tests/${id}/edit`,
    themes: '/themes',
    createQuestion: (themeId: string|number = ':themeId') => 
      `/themes/${themeId}/questions/create`,
    editQuestion: (themeId: string|number = ':themeId', id: string|number = ':id') => 
      `/themes/${themeId}/questions/${id}/edit`,
    createMaterial: 
      '/materials/create',
    editMaterial: (id: string|number = ':id') => 
      `/materials/${id}/edit`,
    studentTests: 
      '/student/tests',
    studentTest: (id: string|number = ':id') => 
      `/student/tests/${id}`,
}

const Routes = () =>
  <Switch>
    <Route exact path={routes.home} component={Home}/>
    <Route exact path={routes.signIn} component={SignIn}/>
    <RouteProxy exact path={routes.account} component={Account} roles={['Admin', 'Lecturer', 'Student']} title='Профиль'/>
    <RouteProxy exact path={routes.tests} component={Tests} roles={['Admin', 'Lecturer']} title='Тесты'/> 
    <RouteProxy exact path={routes.createTest} component={HandleTest} roles={['Admin', 'Lecturer']} title='Создание теста'/>
    <RouteProxy exact path={routes.editTest()} component={HandleTest} roles={['Admin', 'Lecturer']} title='Редактирование теста'/>
    <RouteProxy exact path={routes.themes} component={ThemesPage} roles={['Admin', 'Lecturer']} title='Темы'/>
    <RouteProxy exact path={routes.editQuestion()} component={QuestionHandling} roles={['Admin', 'Lecturer']} title='Редактирование вопроса'/>
    <RouteProxy exact path={routes.createQuestion()} component={QuestionHandling} roles={['Admin', 'Lecturer']} title='Создание вопроса'/>
    <RouteProxy exact path={routes.editMaterial()} component={MaterialHandling} roles={['Admin', 'Lecturer']} title='Редактирование материала'/>
    <RouteProxy exact path={routes.createMaterial} component={MaterialHandling} roles={['Admin', 'Lecturer']} title='Создание материала'/>
    <RouteProxy exact path={routes.studentTests} component={TestSelect} roles={['Student']} title='Выбор теста'/>
    <RouteProxy exact path={routes.studentTest()} component={Test} roles={['Student']} title='Обучение'/>
  </Switch>



export default Routes