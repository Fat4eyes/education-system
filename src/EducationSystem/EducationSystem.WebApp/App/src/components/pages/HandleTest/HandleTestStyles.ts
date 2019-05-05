import {createStyles, Theme} from '@material-ui/core'

const breadcrumbsStyles = (theme: Theme) => ({
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
  }
})

const HandleTestStyles = (theme: Theme) => createStyles({
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50']
  },
  stepper: {
    backgroundColor: theme.palette.grey['100']
  },
  buttonBlock: {
    marginTop: theme.spacing.unit * 3,
    
    '& button': {
      marginRight: theme.spacing.unit * 3
    }
  },
  ...breadcrumbsStyles(theme),
  selectMenu: {
    display: 'flex',
    flexWrap: 'wrap',
    '& div': {
      padding: theme.spacing.unit,
      margin: theme.spacing.unit
    }
  }
})

export default HandleTestStyles