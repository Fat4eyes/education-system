import React, {Component} from 'react'
import NewLayout from '../Layout/New/Layout'
import OldLayout from '../Layout/Old/Layout'
import {createMuiTheme, MuiThemeProvider} from '@material-ui/core/styles';
import env from '../../helpers/env';
import {tealTheme} from '../../helpers/themes';
import {Router} from 'react-router-dom';
import {AuthenticateProvider} from '../../services/authService'
import history from '../../helpers/history'


class App extends Component {
  state = {
    theme: createMuiTheme(tealTheme)
  };

  handleTheme = theme => this.setState({theme: createMuiTheme(theme)});

  render() {
    return <Router history={history}>
      <MuiThemeProvider theme={this.state.theme}>
        <AuthenticateProvider>
          {env.SetOldDesign === 'true' ? <OldLayout/> : <NewLayout handleTheme={this.handleTheme}/>}
        </AuthenticateProvider>
      </MuiThemeProvider>
    </Router>;
  }
}

export default App;