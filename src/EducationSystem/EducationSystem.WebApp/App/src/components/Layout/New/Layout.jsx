import React, {Component} from 'react'
import {
  AppBar,
  Button,
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
import HomeIcon from '@material-ui/icons/Home';
import SignIn from './SignIn/SignIn';
import {Link} from 'react-router-dom';
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider';

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
    const {classes, auth: {isAuthenticated, signOut}} = this.props;
    const isSignIn = isAuthenticated();

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
          <Button color='inherit' size='large' onClick={isSignIn ? signOut : this.handleSingInModal(true)}>
            {isSignIn ? 'Выйти' : 'Войти'}
          </Button>
        </Toolbar>
      </AppBar>
      <SwipeableDrawer
        classes={{paper: classNames(classes.drawerPaper, !this.state.open && classes.drawerPaperClose)}}
        open={this.state.open} onClose={this.handleDrawer(false)} onOpen={this.handleDrawer(true)}>
        <div tabIndex={0} role='button' onClick={this.handleDrawer(false)} onKeyDown={this.handleDrawer(false)}>
          <List>
            <ListItem component={Link} to='/' button>
              <ListItemIcon>
                <HomeIcon/>
              </ListItemIcon>
              <ListItemText primary='Главная'/>
            </ListItem>
            <ListItem component={Link} to='/private' button>
              <ListItemIcon>
                <HomeIcon/>
              </ListItemIcon>
              <ListItemText primary='private'/>
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