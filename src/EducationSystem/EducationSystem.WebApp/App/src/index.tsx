import React, {Suspense} from 'react'
import ReactDOM from 'react-dom'
import {Router} from 'react-router-dom'
import {Loading} from './components/core'
import {blue, edo, grey, purpure} from './themes'
import {unregister} from './serviceWorker'
import history from './history'
import Container from './infrastructure/di/Container'
import Fetch from './helpers/Fetch'
//Providers
import {MuiThemeProvider} from '@material-ui/core/styles'
import {SnackbarProvider} from 'notistack'
import SpinnerProvider, {SpinnerConsumer} from './providers/SpinnerProvider'
import NotificationProvider, {NotificationConsumer} from './providers/NotificationProvider'
import AuthProvider from './providers/AuthProvider/AuthProvider'
//Services
import {TestService} from './services/TestService'
import {DisciplineService} from './services/DisciplineService'
import {ThemeService} from './services/ThemeService'
import {QuestionService} from './services/QuestionService'
import {FileService} from './services/FileService'
import {MaterialService} from './services/MaterialService'
import {NotificationService} from './services/NotificationService'
import {UserService} from './services/UserService'

import './index.less'

const Layout = React.lazy(() => {
  return new Promise<any>(resolve => { //TODO Задержка для дев-тестирования 
    setTimeout(() => resolve(import('./components/Layout/Layout')), 500)
  })
})

unregister()

Container.getContainer()
  .transient(TestService, 'TestService')
  .transient(DisciplineService, 'DisciplineService')
  .transient(ThemeService, 'ThemeService')
  .transient(QuestionService, 'QuestionService')
  .transient(FileService, 'FileService')
  .transient(MaterialService, 'MaterialService')
  .transient(UserService, 'UserService')

let themes = [purpure(), blue(), edo(), grey()]

const App = () =>
  <Router history={history}>
    <MuiThemeProvider theme={themes[Math.floor(Math.random() * themes.length)]}>
      <SnackbarProvider maxSnack={3}>
        <NotificationProvider>
          <SpinnerProvider>
            <NotificationConsumer>
              {notify => {
                Container.getContainer().singleton(NotificationService, 'NotificationService', [notify]).setUp()
                return <SpinnerConsumer>
                  {spinner => {
                    Fetch.instance(spinner, notify.error)
                    return <></>
                  }}
                </SpinnerConsumer>
              }}
            </NotificationConsumer>
            <AuthProvider>
              <Suspense fallback={<Loading/>}>
                <Layout/>
              </Suspense>
            </AuthProvider>
          </SpinnerProvider>
        </NotificationProvider>
      </SnackbarProvider>
    </MuiThemeProvider>
  </Router>

ReactDOM.render(<App/>, document.getElementById('root'))