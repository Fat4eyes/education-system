import * as React from 'react'
import {FunctionComponent} from 'react'
import {
  AppBar,
  createStyles,
  Grid,
  Hidden,
  IconButton,
  Theme,
  Toolbar,
  Typography,
  withStyles,
  WithStyles
} from '@material-ui/core'
import MenuIcon from '@material-ui/icons/Menu'
import {TAuthProps} from '../../providers/AuthProvider/AuthProviderTypes'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider'
import Button from '../stuff/Button'
import {SimpleLink} from '../core'
import {routes} from '../Layout/Routes'
import {headerHeight} from './Layout'
import {MrBlock} from '../stuff/Margin'

const styles = (theme: Theme) => createStyles({
  root: {
    height: headerHeight
  },
  menuButton: {
    marginLeft: -theme.spacing.unit
  },
  signButtonBlock: {
    marginRight: theme.spacing.unit,
    '& p': {
      color: theme.palette.common.white,
      fontSize: '0.8rem'
    }
  },
  fullNameText: {
    color: theme.palette.common.white,
    fontSize: '1.1rem'
    //fontWeight: 'bold'
  }
})

interface IProps {
  onDrawerToggle: () => any
}

type TProps = IProps & WithStyles<typeof styles> & TAuthProps

const Header: FunctionComponent<TProps> = ({classes, onDrawerToggle, auth}: TProps) => {
  const isAuth = auth.checkAuth()

  return <AppBar color='primary' position='sticky' elevation={0} className={classes.root}>
    <Toolbar>
      <Grid container alignItems='center'>
        <Hidden smUp>
          <Grid item>
            <IconButton
              color='inherit'
              aria-label='Open drawer'
              onClick={onDrawerToggle}
              className={classes.menuButton}
            >
              <MenuIcon/>
            </IconButton>
          </Grid>
        </Hidden>
        <Grid item xs/>
        <MrBlock value={2}/>
        <Grid item className={classes.signButtonBlock}>
          {!isAuth
            ? <Button mainColor='blue' component={props => <SimpleLink to={routes.signIn} {...props}/>}>
              <Typography>
                Войти
              </Typography>
            </Button>
            : <Button mainColor='blue' onClick={auth.signOut}>
              <Typography>
                Выйти
              </Typography>
            </Button>
          }
        </Grid>
      </Grid>
    </Toolbar>
  </AppBar>
}

export default withAuthenticated(withStyles(styles)(Header)) as FunctionComponent<IProps>