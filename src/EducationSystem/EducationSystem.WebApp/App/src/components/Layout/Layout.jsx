import React, {Component} from 'react'
import {
  AppBar,
  Drawer,
  IconButton,
  List,
  Menu,
  MenuItem,
  Toolbar,
  Typography,
  withStyles,
  Zoom
} from '@material-ui/core'
import styles from './LayoutStyles'
import SchoolIcon from '@material-ui/icons/School'
import AccountIcon from '@material-ui/icons/AccountCircle'
import HomeIcon from '@material-ui/icons/Home'
import MoreIcon from '@material-ui/icons/MoreVert'
import Grow from '../stuff/Grow'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider'
import If from '../If'
import Routes from './New/Routes/Routes'
import SimpleLink from '../SimpleLink'
import ListItem from '@material-ui/core/ListItem'
import classNames from 'classnames'
import Tooltip from '@material-ui/core/Tooltip'

@withStyles(styles)
@withAuthenticated
class Layout extends Component {
  state = {
    MenuAnchor: null,
    IsLeftMenuOpen: false
  }

  handleInput = ({target: {name, value}}) => this.setState({[name]: value})
  handleMenu = (open, callback) => ({currentTarget}) => {
    this.setState({MenuAnchor: !!open ? currentTarget : null}, () => callback && callback())
  }
  handleLeftMenu = () => this.setState({IsLeftMenuOpen: !this.state.IsLeftMenuOpen})

  render() {
    let {classes, auth: {checkAuth, getFullName, signOut}} = this.props

    const isAuthenticated = checkAuth()

    const LeftMenuItem = ({component, to, Icon, tooltip, closeOnClick}) => 
      <ListItem disableGutters dense onClick={() => !!closeOnClick && this.handleLeftMenu()}>
        <Tooltip title={
          <Typography color='inherit'>{tooltip}</Typography>
        } TransitionComponent={Zoom} placement='right'>
        <IconButton component={component} to={to} color='secondary'>
          <Icon/>
        </IconButton>
      </Tooltip>
    </ListItem>
    
    const LeftMenu = ({closeOnClick}) => <List disablePadding dense className={classes.menuList}>
      <LeftMenuItem component={SimpleLink} to='/' Icon={HomeIcon} tooltip='Главная страница' closeOnClick={closeOnClick}/>
      <If condition={isAuthenticated}>
        <LeftMenuItem component={SimpleLink} to='/account' Icon={AccountIcon} tooltip='Профиль' closeOnClick={closeOnClick}/>
      </If>
    </List>

    return <div className={classes.root}>
      <AppBar position='static'>
        <Toolbar>
          <SchoolIcon className={classNames(classes.staticLeftMenu, classes.leftMenuIcon)}/>
          <IconButton className={classes.leftMenu} onClick={this.handleLeftMenu} color='inherit'>
            <SchoolIcon/>
          </IconButton>
          <Typography variant='h6' color='inherit' className={classes.title}>
            Система обучения
          </Typography>
          <Grow/>
          <If condition={!isAuthenticated}>
            <IconButton className={classes.rightMenuIcon} component={SimpleLink} to='/signin' color='inherit'>
              <AccountIcon/>
            </IconButton>
          </If>
          <If condition={isAuthenticated}>
            <Typography variant='h6' color='inherit'>
              {getFullName(true)}
            </Typography>
            <IconButton color='inherit' onClick={this.handleMenu(true)}>
              <MoreIcon/>
            </IconButton>
            <Menu anchorEl={this.state.MenuAnchor} open={!!this.state.MenuAnchor}>
              <MenuItem component={SimpleLink} to='/account'  onClick={this.handleMenu(false)}>Профиль</MenuItem>
              <MenuItem onClick={this.handleMenu(false, signOut)}>Выйти</MenuItem>
            </Menu>
          </If>
        </Toolbar>
      </AppBar>
      <Drawer
        open
        variant='permanent'
        className={classes.staticLeftMenu}
        classes={{paper: classes.menu}}>
        <LeftMenu/>
      </Drawer>
      <Drawer
        open={this.state.IsLeftMenuOpen}
        variant='permanent'
        className={classNames(classes.drawer, {
          [classes.drawerOpen]: this.state.IsLeftMenuOpen,
          [classes.drawerClose]: !this.state.IsLeftMenuOpen,
        }, classes.leftMenu)}
        classes={{
          paper: classNames(classes.menu, {
            [classes.drawerOpen]: this.state.IsLeftMenuOpen,
            [classes.drawerClose]: !this.state.IsLeftMenuOpen,
          }),
        }}>
        <div className={classNames(classes.curtain, {
          [classes.hide]: !this.state.IsLeftMenuOpen
        })}/>
        <LeftMenu closeOnClick/>
      </Drawer>
      <main className={classes.content}>
        <Routes/>
      </main>
    </div>
  }
}

export default Layout