const styles = theme => {
  console.log(theme)
  let {
    spacing: {unit: spacingUnit},
    mixins: {toolbar}
  } = theme
  
  let drawerWidth = 48
  let contentBasePadding = spacingUnit * 2

  return ({
    root: {
      display: 'block'
    },
    title: {
      marginLeft: theme.spacing.unit,
      [theme.breakpoints.down('xs')]: {
        display: 'none'
      }
    },
    drawer: {
      width: drawerWidth
    },
    menu: {
      top: toolbar.minHeight,
      padding: '0'
    },
    content: {
      padding: contentBasePadding,
      paddingLeft: contentBasePadding + drawerWidth,
      minHeight: `calc(100vh - ${contentBasePadding * 2 + toolbar.minHeight}px)`,
      backgroundColor: theme.palette.grey['200'],
      [theme.breakpoints.down('xs')]: {
        paddingLeft: contentBasePadding
      },
    },
    leftMenu: {
      display: 'none',
      [theme.breakpoints.down('xs')]: {
        display: 'inline'
      },
    },
    leftMenuIcon: {
      paddingLeft: 12
    },
    rightMenuIcon: {
      paddingRight: 12
    },
    staticLeftMenu: {
      display: 'inline',
      [theme.breakpoints.down('xs')]: {
        display: 'none'
      },
    },
    drawerOpen: {
      width: drawerWidth,
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
    },
    drawerClose: {
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
      }),
      overflowX: 'hidden',
      width: 0
    },
    hide: {
      display: 'none'
    },
    curtain: {
      position: 'fixed',
      width: '100%',
      height: '100%',
      backgroundColor: 'rgba(0, 0, 0, 0.3)',
    },
    menuList: {
      zIndex: 1201,
      backgroundColor: theme.palette.grey['100'],
      height: '100%'
    }
  })
}

export default styles