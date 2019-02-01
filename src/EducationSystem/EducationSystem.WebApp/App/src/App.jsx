import React, {Component, Suspense} from 'react'
import {Router} from 'react-router-dom';
import {SnackbarProvider} from 'notistack';
import {createMuiTheme, MuiThemeProvider} from '@material-ui/core/styles';
import env from './helpers/env';
import {tealTheme} from './helpers/themes';
import history from './helpers/history'
import AuthProvider from './providers/AuthProvider/AuthProvider';
import Loading from './Loading';

const NewLayout = React.lazy(() => import('./components/Layout/New/Layout'));
const OldLayout = React.lazy(() => import('./components/Layout/Old/Layout'));

class App extends Component {
  render() {
    return <Router history={history}>
      <MuiThemeProvider theme={createMuiTheme(tealTheme)}>
        <SnackbarProvider maxSnack={3}>
          <AuthProvider>
            <Suspense fallback={<Loading/>}>
              {env.SetOldDesign === 'true' ? <OldLayout/> : <NewLayout handleTheme={this.handleTheme}/>}
            </Suspense>
          </AuthProvider>
        </SnackbarProvider>
      </MuiThemeProvider>
    </Router>
  }
}

export default App;