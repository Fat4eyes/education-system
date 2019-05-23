import React, {PureComponent} from 'react'
import {
  AppBar,
  Button,
  Drawer,
  IconButton,
  List,
  Menu,
  MenuItem,
  Toolbar,
  Typography,
  withStyles
} from '@material-ui/core'
import styles from './LayoutStyles'
import SchoolIcon from '@material-ui/icons/School'
import AccountIcon from '@material-ui/icons/AccountCircle'
import QuestionIcon from '@material-ui/icons/QuestionAnswer'
import HomeIcon from '@material-ui/icons/Home'
import PlaylistAddIcon from '@material-ui/icons/PlaylistAdd'
import NoteAddIcon from '@material-ui/icons/NoteAdd'
import MoreIcon from '@material-ui/icons/MoreVert'
import Grow from '../stuff/Grow'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider'
import Routes, {routes} from './Routes'
import {If, SimpleLink} from '../core'
import ListItem from '@material-ui/core/ListItem'
import classNames from 'classnames'
import Tooltip from '@material-ui/core/Tooltip'
import Zoom from '@material-ui/core/Zoom'
import withWidth, {isWidthDown} from '@material-ui/core/withWidth'
import * as Handlers from '../../helpers/Handlers'
import Scrollbar from '../stuff/Scrollbar'

@withWidth()
@withStyles(styles)
@withAuthenticated
class Layout extends PureComponent {
  state = {
    MenuAnchor: null,
    IsLeftMenuOpen: false
  }

  handleInput = ({target: {name, value}}) => this.setState({[name]: value})
  handleMenu = (callback) => ({currentTarget}) => {
    this.setState({MenuAnchor: !!this.state.MenuAnchor ? null : currentTarget}, () => callback && callback())
  }
  handleLeftMenu = () => this.setState({IsLeftMenuOpen: !this.state.IsLeftMenuOpen})

  render() {
    let {classes, auth: {checkAuth, User, signOut}} = this.props
    const isAuthenticated = checkAuth()
    let isXs = isWidthDown('xs', this.props.width)

    const LeftMenuItem = ({component, to, Icon, tooltip, closeOnClick}) =>
      <ListItem button={!!isXs}
                disableGutters={!isXs}
                dense
                onClick={() => !!closeOnClick && this.handleLeftMenu()}
                className={classes.listItem}
      >
        <If condition={isXs} orElse={
          <Tooltip className={classes.staticLeftMenu} title={
            <Typography color='inherit'>{tooltip}</Typography>
          } TransitionComponent={Zoom} placement='right'>
            <IconButton component={component} to={to} color='secondary'>
              <Icon/>
            </IconButton>
          </Tooltip>
        }>
          <Button disableRipple size='small' component={component} to={to} color='secondary'>
            <Icon/><Typography component='span' noWrap color='inherit'>{tooltip}</Typography>
          </Button>
        </If>
      </ListItem>

    const LeftMenu = ({closeOnClick}) => <List disablePadding className={classes.menuList}>
      <LeftMenuItem component={SimpleLink} to='/' Icon={HomeIcon} tooltip='Главная'
                    closeOnClick={closeOnClick}/>
      <If condition={isAuthenticated}>
        <LeftMenuItem component={SimpleLink} to={routes.account} Icon={AccountIcon} tooltip='Профиль'
                      closeOnClick={closeOnClick}/>
      </If>
      {
        User.Roles && <>
          <If condition={User.Roles.Admin || User.Roles.Lecturer}>
            <LeftMenuItem component={SimpleLink} to={routes.tests} Icon={QuestionIcon} tooltip='Тесты'
                          closeOnClick={closeOnClick}/>
          </If>
          <If condition={User.Roles.Lecturer}>
            <LeftMenuItem component={SimpleLink} to={routes.createTest} Icon={PlaylistAddIcon} tooltip='Добавить тест'
                          closeOnClick={closeOnClick}/>
          </If>
          <If condition={User.Roles.Admin || User.Roles.Lecturer}>
            <LeftMenuItem component={SimpleLink} to={routes.themes} Icon={PlaylistAddIcon} tooltip='Темы'
                          closeOnClick={closeOnClick}/>
            <LeftMenuItem component={SimpleLink} to={routes.createMaterial} Icon={NoteAddIcon} tooltip='Material'
                          closeOnClick={closeOnClick}/>
          </If>
          <If condition={User.Roles.Student}>
            <LeftMenuItem component={SimpleLink} to={routes.studentTests} Icon={QuestionIcon} tooltip='Тесты'
                          closeOnClick={closeOnClick}/>
          </If>
        </>
      }
    </List>

    return <div className={classes.root}>

      <AppBar position='static'>
        <Toolbar>
          <If condition={isXs} orElse={<div className={classes.leftMenuIcon}><SchoolIcon/></div>}>
            <IconButton onClick={this.handleLeftMenu} color='inherit'>
              <SchoolIcon/>
            </IconButton>
          </If>
          <Typography variant='h6' color='inherit' className={classes.title}>
            Система обучения
          </Typography>
          <Grow/>
          <If condition={isAuthenticated} orElse={
            <IconButton className={classes.rightMenuIcon} component={SimpleLink} to={routes.signIn} color='inherit'>
              <AccountIcon/>
            </IconButton>
          }>
            <Typography variant='h6' color='inherit'>
              {Handlers.getFullName(User, true)}
            </Typography>
            <IconButton className={classes.rightMenuIcon} color='inherit' onClick={this.handleMenu()}>
              <MoreIcon/>
            </IconButton>
            <Menu classes={{paper: classes.moreMenu}}
                  anchorEl={this.state.MenuAnchor}
                  open={!!this.state.MenuAnchor}
                  onClose={this.handleMenu()}>
              <MenuItem component={SimpleLink} to={routes.account} onClick={this.handleMenu()}>Профиль</MenuItem>
              <MenuItem onClick={this.handleMenu(signOut)}>Выйти</MenuItem>
            </Menu>
          </If>
        </Toolbar>
      </AppBar>
      <If condition={isXs} orElse={
        <Drawer open variant='permanent' classes={{paper: classes.menu}}>
          <LeftMenu/>
        </Drawer>
      }>
        <Drawer
          open={this.state.IsLeftMenuOpen}
          variant='permanent'
          className={classNames(classes.drawer, {
            [classes.drawerOpen]: this.state.IsLeftMenuOpen,
            [classes.drawerClose]: !this.state.IsLeftMenuOpen
          })}
          classes={{
            paper: classNames(classes.menu, {
              [classes.drawerOpen]: this.state.IsLeftMenuOpen,
              [classes.drawerClose]: !this.state.IsLeftMenuOpen
            })
          }}>
          <div onClick={this.handleLeftMenu} className={classNames(classes.curtain, {
            [classes.hide]: !this.state.IsLeftMenuOpen
          })}/>
          <LeftMenu closeOnClick/>
        </Drawer>
      </If>
      <Scrollbar className={classes.scrollbar}>
        <main className={classes.content}>
          <Routes/>
        </main>
      </Scrollbar>
    </div>
  }
}

export default Layout