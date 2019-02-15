import React, {Suspense} from 'react'
import ReactDOM from 'react-dom'
import {Router} from 'react-router-dom'
import {MuiThemeProvider} from '@material-ui/core/styles'
import {SnackbarProvider} from 'notistack'
import AuthProvider from './providers/AuthProvider/AuthProvider'
import {Loading, Try} from './components/core'
import {blue} from './themes'
import {unregister} from './serviceWorker'
import './index.less'
import environment from './environment'
import history from './history'

// console.log(environment)

const Layout = React.lazy(() => { 
  return new Promise(resolve => { //TODO Задержка для дев-тестирования 
    setTimeout(() => resolve(import('./components/Layout/Layout')), 500)
  })
})

unregister()

const App = () => <Try>
  <Router history={history}>
    <MuiThemeProvider theme={blue()}>
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