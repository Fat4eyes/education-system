const TestsTableStyles = theme => {
  const userSelectNone = {
    '-webkit-touch-callout': 'none',
    '-webkit-user-select': 'none',
    '-khtml-user-select': 'none',
    '-moz-user-select': 'none',
    '-ms-user-select': 'none',
    'user-select': 'none' 
  }
  
  return ({
    main: {
      [theme.breakpoints.down('md')]: {
        flexDirection: 'row'
      },
      [theme.breakpoints.up('md')]: {
        flexDirection: 'row-reverse'
      }

    },
    paper: {
      padding: theme.spacing.unit * 3,
      backgroundColor: theme.palette.grey['50']
    },
    tableContainer: {},
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
    titleSelected: {
      color: 'white'
    },
    titleNotSelected: {
      color: theme.palette.secondary.dark
    },
    select: {
      '&::before': {
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
        width: 150
      },
      [theme.breakpoints.up('sm')]: {
        width: 400
      },
      [theme.breakpoints.up('md')]: {
        width: 550
      },
      [theme.breakpoints.up('lg')]: {
        width: 700
      }
    },
    row: {
      margin: `${theme.spacing.unit / 2}px 0 0`
    },
    rowHeader: {
      ...userSelectNone,
      cursor: 'pointer',
      padding: `${theme.spacing.unit}px ${theme.spacing.unit * 2}px ${theme.spacing.unit / 2}px`,
      backgroundColor: theme.palette.grey['200'],
      '&:hover': {
        backgroundColor: theme.palette.grey['300']
      }
    },
    rowHeaderSelected: {
      backgroundColor: theme.palette.primary.main + ' !important',
      '& h6': {
        color: 'white'
      }
    },
    rowProgress: {
      height: theme.spacing.unit / 2
    },
    collapse: {
      width: '100%'
    }
  })
}

export default TestsTableStyles