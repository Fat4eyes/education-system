const styles = theme => {
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
      width: drawerWidth,
      [theme.breakpoints.down('xs')]: {
        width: '45%'
      },
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
    MenuItemName: {
      display: 'none',
      [theme.breakpoints.down('xs')]: {
        display: 'inline'
      },
    },
    leftMenuIcon: {
      paddingLeft: 12,
      marginRight: 5
    },
    rightMenuIcon: {
      paddingRight: 12,
      marginLeft: 5
    },
    staticLeftMenu: {
      display: 'inline',
      [theme.breakpoints.down('xs')]: {
        display: 'none'
      },
    },
    drawerOpen: {
      width: drawerWidth,
      [theme.breakpoints.down('xs')]: {
        width: '45%'
      },
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
    curtainGlobal: {
      position: 'fixed',
      top: 0,
      width: '100%',
      height: '100%',
      zIndex: 1201
    },
    menuList: {
      zIndex: 1203,
      backgroundColor: theme.palette.grey['100'],
      height: '100%',
      padding: '0 5px',
      [theme.breakpoints.down('xs')]: {
        padding: 0
      }
    },
    moreMenu: {
      top: `${toolbar.minHeight}px !important`,
      right: 0
    },
    hover: {
      '&:hover' : {
        transition: 'background-color 150ms cubic-bezier(0.4, 0, 0.2, 1) 0ms'
      }
    }
  })
}

export default styles