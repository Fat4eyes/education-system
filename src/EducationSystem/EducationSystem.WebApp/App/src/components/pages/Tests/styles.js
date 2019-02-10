const styles = theme => ({
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50'],
  },
  main: {
    [theme.breakpoints.down('xs')]: {
      flexDirection: 'row'
    },
    [theme.breakpoints.up('lg')]: {
      flexDirection: 'row-reverse'
    },

  },
  form: {
    minWidth: "100%"
  },
  formControl: {
    margin: theme.spacing.unit,
    minWidth: `calc(100% - ${theme.spacing.unit * 2}px)`,
  },
  expansionPanel: {
    backgroundColor: theme.palette.grey['50'],
  },
  expansionPanelSummary: {
    [theme.breakpoints.down('xs')]: {
      // minHeight: '30px !important',
      // height: '30px !important',
    }
  },
  expansionPanelDetails: {
    padding: `0 ${theme.spacing.unit * 2}px ${theme.spacing.unit * 3}px !important`
  }
});

export default styles