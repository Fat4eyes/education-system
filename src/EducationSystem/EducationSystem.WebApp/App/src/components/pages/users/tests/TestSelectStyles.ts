import {createStyles, Theme} from '@material-ui/core'

export const TestSelectStyles = (theme: Theme) => createStyles({
  header: {
    cursor: 'pointer',
    backgroundColor: theme.palette.primary.main,
    padding: `${theme.spacing.unit}px 0 !important`,
    color: theme.palette.primary.contrastText
  },
  body: {
    padding: `${theme.spacing.unit}px 0 !important`
  },
  mt2Unit: {
    marginTop: theme.spacing.unit * 2
  },
  mainBodyBlock: {
    padding: `0 ${theme.spacing.unit * 3}px !important`
  },
  clikableBlock: {
    cursor: 'pointer',
    '-webkit-touch-callout': 'none',
    '-webkit-user-select': 'none',
    '-khtml-user-select': 'none',
    '-moz-user-select': 'none',
    '-ms-user-select': 'none',
    'user-select': 'none'
  },
  bodyBlock: {
    padding: theme.spacing.unit * 2,
  }
})