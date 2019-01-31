import React, {Component} from 'react'
import NewLayout from '../Layout/New/Layout'
import OldLayout from '../Layout/Old/Layout'
import {createMuiTheme, MuiThemeProvider} from '@material-ui/core/styles';
import env from '../../helpers/env';
import {tealTheme} from '../../helpers/themes';
import {Router} from 'react-router-dom';
import history from '../../helpers/history'
import {SnackbarProvider} from 'notistack';
import AuthProvider from '../../providers/AuthProvider/AuthProvider';

class App extends Component {
  state = {
    theme: createMuiTheme(tealTheme)
  };

  handleTheme = theme => this.setState({theme: createMuiTheme(theme)});

  render() {
    return <SnackbarProvider maxSnack={3}>
      <Router history={history}>
        <MuiThemeProvider theme={this.state.theme}>
          <AuthProvider>
            {env.SetOldDesign === 'true' ? <OldLayout/> : <NewLayout handleTheme={this.handleTheme}/>}
          </AuthProvider>
        </MuiThemeProvider>
      </Router>
    </SnackbarProvider>;
  }
}

export default App;