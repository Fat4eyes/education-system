import React, {Component} from 'react'
import {
  AppBar,
  IconButton,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Paper,
  SwipeableDrawer,
  Toolbar,
  Typography,
  withStyles
} from '@material-ui/core';
import classNames from 'classnames';
import styles from './styles'
import Routes from './Routes/Routes';
import MenuIcon from '@material-ui/icons/Menu';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import Face from '@material-ui/icons/Face';
import HomeIcon from '@material-ui/icons/Home';
import SignIn from './SignIn/SignIn';
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider';
import Authenticated from '../../../providers/AuthProvider/Authenticated';
import SimpleLink from "../../SimpleLink";

@withAuthenticated
@withStyles(styles)
class Layout extends Component {
  state = {
    open: false,
    singInModalOpen: false
  };

  handleDrawer = value => () => this.setState({open: value});
  handleSingInModal = value => () => this.setState({singInModalOpen: value});

  render() {
    const {classes, auth: {isAuthenticated: checkAuth, signOut, getFullName}} = this.props;
    const isAuthenticated = checkAuth();

    return <div className={classes.root}>
      <AppBar className={classNames(classes.appBar, this.state.open && classes.appBarShift)}>
        <Toolbar variant='dense' disableGutters={!this.state.open} className={classes.toolbar}>
          <IconButton color='inherit' onClick={this.handleDrawer(true)}
                      className={classNames(classes.menuButton, this.state.open && classes.menuButtonHidden)}>
            <MenuIcon/>
          </IconButton>
          <Typography variant='h6' color='inherit' noWrap className={classes.title}>
            Система обучения
          </Typography>
          <Authenticated>
            <div>
              <Typography component="p" color='inherit' noWrap className={classes.fullName}>
                {getFullName()}
              </Typography>
            </div>
          </Authenticated>
          <IconButton color='inherit' size='large' onClick={isAuthenticated ? signOut : this.handleSingInModal(true)}>
            {isAuthenticated ? <ExitToAppIcon/> : <Face/>}
          </IconButton>
        </Toolbar>
      </AppBar>
      <SwipeableDrawer
        classes={{paper: classNames(classes.drawerPaper, !this.state.open && classes.drawerPaperClose)}}
        open={this.state.open} onClose={this.handleDrawer(false)} onOpen={this.handleDrawer(true)}>
        <div tabIndex={0} role='button' onClick={this.handleDrawer(false)} onKeyDown={this.handleDrawer(false)}>
          <List>
            <ListItem component={SimpleLink} to='/' button>
              <ListItemIcon>
                <HomeIcon/>
              </ListItemIcon>
              <ListItemText primary='Главная'/>
            </ListItem>
            <ListItem component={SimpleLink} to='/private' button>
              <ListItemIcon>
                <HomeIcon/>
              </ListItemIcon>
              <ListItemText primary='Приватная'/>
            </ListItem>
          </List>
        </div>
      </SwipeableDrawer>
      <main className={classes.content}>
        <Paper elevation={0} square={true} className={classes.page}>
          <Routes loginHandler={this.handleSingInModal(true)}/>
        </Paper>
      </main>
      <SignIn open={this.state.singInModalOpen} handleClose={this.handleSingInModal(false)}/>
    </div>;
  }
}

export default Layout