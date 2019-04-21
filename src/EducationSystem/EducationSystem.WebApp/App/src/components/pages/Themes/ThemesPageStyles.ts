import {createStyles, Theme} from '@material-ui/core'

const ThemesPageStyles = (theme: Theme) => createStyles({
  paperSmall: {
    padding: theme.spacing.unit * 2,
    backgroundColor: theme.palette.grey['50']
  },
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50']
  },
  rowProgress: {
    height: theme.spacing.unit / 2
  },
  header: {
    backgroundColor: theme.palette.primary.main,
    padding: `${theme.spacing.unit}px ${theme.spacing.unit * 3}px !important`,
    color: theme.palette.primary.contrastText
  },
  breadcrumbs: {
    padding: `0 ${theme.spacing.unit}px !important`,
    backgroundColor: theme.palette.primary.main,
    cursor: 'default'
  },
  breadcrumbsClickable: {
    cursor: 'pointer',
    borderColor: theme.palette.grey['400'],
    borderRadius: 50,
    '&:hover': {
      backgroundColor: theme.palette.primary.light
    }
  },
  headerText: {
    color: theme.palette.primary.contrastText,
  },
  mr2Unit: {
    marginRight: theme.spacing.unit * 2
  },
  mrUnit: {
    marginRight: theme.spacing.unit
  },
  mt2Unit: {
    marginTop: theme.spacing.unit * 2
  }
})

export default ThemesPageStyles