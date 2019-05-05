import {createStyles, Theme} from '@material-ui/core'

const TestsTableStyles = (theme: Theme) => {
  const userSelectNone = {
    '-webkit-touch-callout': 'none',
    '-webkit-user-select': 'none',
    '-khtml-user-select': 'none',
    '-moz-user-select': 'none',
    '-ms-user-select': 'none',
    'user-select': 'none'
  }
  
  return createStyles({
    main: {
      [theme.breakpoints.down('md')]: {
        flexDirection: 'row'
      },
      [theme.breakpoints.up('md')]: {
        flexDirection: 'row-reverse'
      }

    },
    header: {
      backgroundColor: theme.palette.primary.main,
      padding: `${theme.spacing.unit}px ${theme.spacing.unit * 3}px !important`,
      color: theme.palette.primary.contrastText
    },
    headerText: {
      color: theme.palette.primary.contrastText
    },
    paper: {
      padding: theme.spacing.unit * 4.5,
    },
    loading: {
      width: 100,
      position: 'relative',
      left: '50%',
      top: '50%',
      padding: '10px 0',
      transform: 'translate(-50%, -50%)'
    },
    loadingBlock: {
      padding: '0 5px 0 4px',
      height: 4
    },
    row: {
      margin: `${theme.spacing.unit / 2}px 0 0`
    },
    rowHeader: {
      ...userSelectNone,
      cursor: 'pointer',
      padding: `${theme.spacing.unit + 4}px ${theme.spacing.unit * 2}px ${theme.spacing.unit / 2 + 4}px`,
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
    },
    content: {
      margin: `${theme.spacing.unit * 3}px 0`
    },
    rowDetails: {
      backgroundColor: theme.palette.grey['50'],
      padding: `${theme.spacing.unit * 2}px`,
      borderBottom: `1px solid`,
      borderBottomColor: theme.palette.grey['500'],
    },
    rowDetailsHeader: {
      color: theme.palette.primary.main
    },
    modal: {
      position: 'absolute',
      width: 300,
      top: `50%`,
      left: `50%`,
      transform: `translate(-50%, -50%)`,
      padding: 1
    }
  })
}

export default TestsTableStyles