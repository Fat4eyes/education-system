import * as React from 'react'
import {FunctionComponent, useState} from 'react'
import {createStyles, CssBaseline, Theme, withStyles, WithStyles} from '@material-ui/core'
import {Hidden} from '@material-ui/core/es'
import Navigator from './Navigator'
import Header from './Header'
import Content from './Content'

export const drawerWidth = 224
export const headerHeight = 50
const styles = (theme: Theme) => createStyles({
  root: {
    display: 'flex',
    minHeight: '100vh'
  },
  drawer: {
    [theme.breakpoints.up('sm')]: {
      width: drawerWidth,
      flexShrink: 0
    }
  },
  appContent: {
    flex: 1,
    display: 'flex',
    flexDirection: 'column'
  },
  mainContent: {
    flex: 1,
    background: '#eaeff1'
  }
})

interface IProps {
}

type TProps = IProps & WithStyles<typeof styles>

const Layout: FunctionComponent<TProps> = ({classes}: TProps) => {
  const [mobileOpen, setMobileOpen] = useState<boolean>(false)
  const handleDrawerToggle = () => setMobileOpen(!mobileOpen)

  return <div className={classes.root}>
    <CssBaseline/>
    <nav className={classes.drawer}>
      <Hidden smUp implementation='js'>
        <Navigator
          PaperProps={{style: {width: drawerWidth}}}
          variant='temporary'
          open={mobileOpen}
          onClose={handleDrawerToggle}
        />
      </Hidden>
      <Hidden xsDown implementation='css'>
        <Navigator PaperProps={{style: {width: drawerWidth}}}/>
      </Hidden>
    </nav>
    <div className={classes.appContent}>
      <Header onDrawerToggle={handleDrawerToggle}/>
      <main className={classes.mainContent}>
        <Content/>
      </main>
    </div>
  </div>
}

export default withStyles(styles)(Layout) as FunctionComponent<IProps>