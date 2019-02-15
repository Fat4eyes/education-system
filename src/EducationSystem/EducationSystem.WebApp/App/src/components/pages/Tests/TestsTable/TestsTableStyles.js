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
  tableContainer: {
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
    },
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
  },
  loading: {
    width: 100,
    position: 'relative',
    left: '50%',
    top: '50%',
    padding: '10px 0',
    transform: 'translate(-50%, -50%)'
  },
  cursor: {
    cursor: 'pointer'
  },
  loadingBlock: {
    padding: '0 5px 0 4px',
    height: 4
  },
  header: {
    [theme.breakpoints.up('xs')]: {
      width: 250
    },
    [theme.breakpoints.up('sm')]: {
      width: 400
    },
    [theme.breakpoints.up('md')]: {
      width: 600
    },
    [theme.breakpoints.up('lg')]: {
      width: 800
    }
  }
})

export default TestsTableStyles