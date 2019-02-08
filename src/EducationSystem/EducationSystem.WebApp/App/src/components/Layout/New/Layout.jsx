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
  ButtonBase,
  withStyles
} from '@material-ui/core';
import classNames from 'classnames';
import styles from './styles'
import Routes from './Routes/Routes';
import MenuIcon from '@material-ui/icons/Menu';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import Face from '@material-ui/icons/Face';
import HomeIcon from '@material-ui/icons/Home';
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider';
import Authenticated from '../../../providers/AuthProvider/Authenticated';
import SimpleLink from '../../SimpleLink';

@withAuthenticated
@withStyles(styles)
class Layout extends Component {
  state = {open: false};

  handleDrawer = value => () => this.setState({open: value});

  render() {
    const {
      classes,
      auth: {
        checkAuth,
        signOut,
        getFullName,
        openAuthModal
      }
    } = this.props;
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
            <ButtonBase component={SimpleLink} to='/account' className={classes.accountButton}>
              <Typography component='p' color='inherit' noWrap className={classes.fullName}>
                {getFullName(true)}
              </Typography>
            </ButtonBase>
          </Authenticated>
          <IconButton color='inherit' size='large' onClick={isAuthenticated ? signOut : openAuthModal}>
            {isAuthenticated ? <ExitToAppIcon/> : <Face/>}
          </IconButton>
        </Toolbar>
      </AppBar>
      <SwipeableDrawer
        classes={{paper: classNames(classes.drawerPaper, !this.state.open && classes.drawerPaperClose)}}
        open={this.state.open} onClose={this.handleDrawer(false)} onOpen={this.handleDrawer(true)}>
        <div tabIndex={0} role='button' onClick={this.handleDrawer(false)} onKeyDown={this.handleDrawer(false)}>
          <List component="nav">
            <ListItem component={SimpleLink} to='/' button>
              <ListItemIcon>
                <HomeIcon/>
              </ListItemIcon>
              <ListItemText primary='Главная'/>
            </ListItem>
            <Authenticated>
              <ListItem component={SimpleLink} to='/tests' button>
                <ListItemIcon>
                  <Face/>
                </ListItemIcon>
                <ListItemText primary='Аккаунт'/>
              </ListItem>
              <ListItem component={SimpleLink} to='/account' button>
                <ListItemIcon>
                  <Face/>
                </ListItemIcon>
                <ListItemText primary='Аккаунт'/>
              </ListItem>
            </Authenticated>
          </List>
        </div>
      </SwipeableDrawer>
      <main className={classes.content}>
        <Paper elevation={0} square={true} className={classes.page}>
          <Routes/>
        </Paper>
      </main>
    </div>;
  }
}

export default Layout