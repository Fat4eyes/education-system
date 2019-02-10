const TestsTableStyles = theme => ({
  main: {
    [theme.breakpoints.down('xs')]: {
      flexDirection: 'row'
    },
    [theme.breakpoints.up('lg')]: {
      flexDirection: 'row-reverse'
    },

  },
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50'],
  },
  root: {
    borderSpacing: `${theme.spacing.unit / 2}px !important`,
    borderCollapse: 'separate !important',
    '& tr': {
      backgroundColor: theme.palette.grey['200'], 
      '& td': {
        borderBottom: 'none'
      }
    },
    '& th': {
      borderBottom: 'none'
    }
  }, 
  footer: {
    '& tr': {
      backgroundColor: theme.palette.grey['50'] + '!important'
    }
  },
  pagination: {
    padding: `0 ${theme.spacing.unit}px`
  },
  selected: {
    backgroundColor: theme.palette.primary.main + '!important'
  },
  testDetails: {
    backgroundColor: theme.palette.grey['50'] + '!important'
  },
  titleSelected: {
    color: 'white'
  },
  titleNotSelected: {
    color: theme.palette.secondary.dark
  },
  select: {
    '&::before':{
      borderBottom: 'none'
    }
  }
})

export default TestsTableStyles