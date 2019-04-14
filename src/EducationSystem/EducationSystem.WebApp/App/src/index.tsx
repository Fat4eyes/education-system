import React, {Suspense} from 'react'
import ReactDOM from 'react-dom'
import {Router} from 'react-router-dom'
import {MuiThemeProvider} from '@material-ui/core/styles'
import {SnackbarProvider} from 'notistack'
import AuthProvider from './providers/AuthProvider/AuthProvider'
import {Loading, Try} from './components/core'
import {blue, edo, grey, purpure} from './themes'
import {unregister} from './serviceWorker'
import './index.less'
import history from './history'
import Container from './infrastructure/di/Container'
import TestService from './services/implementations/TestService'
import DisciplineService from './services/implementations/DisciplineService'
import ThemeService from './services/implementations/ThemeService'
import QuestionService from './services/implementations/QuestionService'
import FileService from './services/implementations/FileService'
import MaterialService from './services/implementations/MaterialService'
import NotificationProvider from './providers/NotificationProvider'
import StudentService from './services/implementations/StudentService'

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
  .transient(StudentService, 'StudentService')
  .setUp()

let themes = [purpure(), blue(), edo(), grey()]

const App = () => <Try>
  <Router history={history}>
    <MuiThemeProvider theme={themes[Math.floor(Math.random() * themes.length)]}>
      <SnackbarProvider maxSnack={3}>
        <NotificationProvider>
          <AuthProvider>
            <Suspense fallback={
              <Loading/>
            }>
              <Layout/>
            </Suspense>
          </AuthProvider>
        </NotificationProvider>
      </SnackbarProvider>
    </MuiThemeProvider>
  </Router>
</Try>

ReactDOM.render(<App/>, document.getElementById('root'))