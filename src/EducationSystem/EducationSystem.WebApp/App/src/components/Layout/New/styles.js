const drawerWidth = 180;
const toolbarHeight = 48;

const styles = theme => ({
  root: {
    display: 'flex',
  },
  toolbar: {
    paddingRight: 24,
    minHeight: toolbarHeight
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  appBarShift: {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  menuButton: {
    marginLeft: 12,
    marginRight: 36,
  },
  menuButtonHidden: {
    display: 'none',
  },
  title: {
    flexGrow: 1,
  },
  drawerPaper: {
    position: 'relative',
    whiteSpace: 'nowrap',
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  drawerPaperClose: {
    overflowX: 'hidden',
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    width: theme.spacing.unit * 7,
    [theme.breakpoints.up('sm')]: {
      width: theme.spacing.unit * 9,
    },
  },
  appBarSpacer: {
    ...theme.mixins.toolbar
  },
  content: {
    flexGrow: 1,
    height: '100vh',
    overflow: 'auto',
    backgroundColor: theme.palette.grey["200"]
  },
  page: {
    padding: theme.spacing.unit * 2,
    backgroundColor: theme.palette.grey["200"],
    border: 'dashed 0.5px black',

    minHeight: `calc(100% - ${toolbarHeight + theme.spacing.unit * 8}px)`,
    margin: `${toolbarHeight + theme.spacing.unit * 2}px 5% ${theme.spacing.unit * 2}px`,
    display: 'flex'
  },
  fullName: {
    fontSize: 18,
    marginRight: theme.spacing.unit
  },
  accountButton: {
    padding: 10,
    borderRadius: 30
  }
});

export default styles;