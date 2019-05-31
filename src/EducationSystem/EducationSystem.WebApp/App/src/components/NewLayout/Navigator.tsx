import * as React from 'react'
import {FunctionComponent} from 'react'
import {
  createStyles,
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Theme,
  withStyles,
  WithStyles
} from '@material-ui/core'
import HomeIcon from '@material-ui/icons/Home'
import classNames from 'classnames'
import {DrawerProps} from '@material-ui/core/Drawer'
import {SimpleLink} from '../core'
import AccountIcon from '@material-ui/icons/AccountCircle'
import QuestionIcon from '@material-ui/icons/QuestionAnswer'
import PlaylistAddIcon from '@material-ui/icons/PlaylistAdd'
import NoteAddIcon from '@material-ui/icons/NoteAdd'
import {routes} from '../Layout/Routes'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider'
import {TAuthProps} from '../../providers/AuthProvider/AuthProviderTypes'
import {TRoles} from '../../models/User'
import {headerHeight} from './Layout'
import ReplyIcon from '@material-ui/icons/Reply'
import {testingSystemRoutes} from '../../routes'

const styles = (theme: Theme) => createStyles({
  drawer: {
    '&>div': {
      backgroundColor: theme.palette.primary.main
    }
  },
  list: {
    height: '100%'
  },
  categoryHeader: {
    paddingTop: 16,
    paddingBottom: 16
  },
  categoryHeaderPrimary: {
    color: theme.palette.common.white
  },
  item: {
    paddingTop: theme.spacing.unit,
    paddingBottom: theme.spacing.unit,
    color: theme.palette.primary.contrastText,
    '& svg': {
      color: theme.palette.primary.contrastText
    }
  },
  itemCategory: {
    backgroundColor: theme.palette.primary.main,
    boxShadow: '0 -1px 0 #404854 inset',
    paddingTop: 16,
    paddingBottom: 16
  },
  firebase: {
    height: headerHeight,
    fontSize: '1.3em',
    fontFamily: theme.typography.fontFamily,
    color: theme.palette.primary.contrastText,
    marginBottom: theme.spacing.unit * 2
  },
  itemActionable: {
    '&:hover': {
      backgroundColor: 'rgba(255, 255, 255, 0.08)'
    }
  },
  itemActiveItem: {
    color: '#4fc3f7'
  },
  itemPrimary: {
    color: 'inherit',
    fontSize: theme.typography.fontSize,
    '&$textDense': {
      fontSize: theme.typography.fontSize
    }
  },
  textDense: {},
  divider: {
    marginTop: theme.spacing.unit * 2
  },
  bottomBlock: {
    position: 'absolute',
    bottom: theme.spacing.unit * 2
  }
})

interface IItemProps {
  Icon: any
  title: string
  to: string
}

interface IProps extends DrawerProps {}

type TProps = IProps & WithStyles<typeof styles> & TAuthProps
const Navigator: FunctionComponent<TProps> = ({classes, auth, ...props}: TProps) => {
  const Item = withStyles(styles)(
    ({classes, Icon, title, to}: IItemProps & WithStyles<typeof styles>) =>
      <ListItem button dense className={classNames(classes.item, classes.itemActionable)}
                component={props => <SimpleLink to={to} {...props}/>}
                onClick={props.onClose}
      >
        <ListItemIcon><Icon/></ListItemIcon>
        <ListItemText classes={{primary: classes.itemPrimary, textDense: classes.textDense}}>
          {title}
        </ListItemText>
      </ListItem>
  ) as FunctionComponent<IItemProps>

  const hasRoles = !!auth.User && !!auth.User.Roles
  return <Drawer variant='permanent' {...props} className={classes.drawer}>
    <List disablePadding className={classes.list}>
      <ListItem className={classNames(classes.firebase, classes.item, classes.itemCategory)}>
        Система обучения
      </ListItem>
      <Item title='Главная' to='/' Icon={HomeIcon}/>
      {hasRoles && (({Admin, Lecturer, Student}: TRoles) => {
        const isAdminOrLecturer = Admin || Lecturer
        return <>
          <Item title='Профиль' to={routes.account} Icon={AccountIcon}/>
          {isAdminOrLecturer && <Item title='Тесты' to={routes.tests} Icon={QuestionIcon}/>}
          {isAdminOrLecturer && <Item title='Темы' to={routes.themes} Icon={PlaylistAddIcon}/>}
          {isAdminOrLecturer && <Item title='Материалы' to={routes.materials} Icon={NoteAddIcon}/>}
          {Student && <Item title='Тесты' to={routes.studentTests} Icon={QuestionIcon}/>}
        </>
      })(auth.User!.Roles)}
      <ListItem button dense
                className={classNames(classes.bottomBlock, classes.item, classes.itemActionable)}
                component={props => <a href={testingSystemRoutes.main()} target='_blank' {...props}/>}
      >
        <ListItemIcon><ReplyIcon/></ListItemIcon>
        <ListItemText classes={{primary: classes.itemPrimary, textDense: classes.textDense}}>
          Система тестирования
        </ListItemText>
      </ListItem>
    </List>
  </Drawer>
}

export default withAuthenticated(
  withStyles(styles)(
    Navigator
  )
) as FunctionComponent<IProps>