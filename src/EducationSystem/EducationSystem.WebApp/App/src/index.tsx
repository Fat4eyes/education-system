import React, {Suspense} from 'react'
import ReactDOM from 'react-dom'
import {Router} from 'react-router-dom'
import {MuiThemeProvider} from '@material-ui/core/styles'
import {SnackbarProvider} from 'notistack'
import AuthProvider from './providers/AuthProvider/AuthProvider'
import {Loading, Try} from './components/core'
import {blue, purpleVaginaForVitya} from './themes'
import {unregister} from './serviceWorker'
import './index.less'
import history from './history'
import Container from './infrastructure/di/Container'
import TestService from './services/implementations/TestService'
import DisciplineService from './services/implementations/DisciplineService'
import ThemeService from './services/implementations/ThemeService'

const Layout = React.lazy(() => {
  return new Promise<any>(resolve => { //TODO Задержка для дев-тестирования 
    setTimeout(() => resolve(import('./components/Layout/Layout')), 500)
  })
})

unregister()

Container.getContainer()
  .transient(TestService, TestService.name)
  .transient(DisciplineService, DisciplineService.name)
  .transient(ThemeService, ThemeService.name)
  .setUp()

const App = () => <Try>
  <Router history={history}>
    <MuiThemeProvider theme={purpleVaginaForVitya()}>
      <SnackbarProvider maxSnack={3}>
        <AuthProvider>
          <Suspense fallback={
            <Loading/>
          }>
            <Layout/>
          </Suspense>
        </AuthProvider>
      </SnackbarProvider>
    </MuiThemeProvider>
  </Router>
</Try>

ReactDOM.render(<App/>, document.getElementById('root'))