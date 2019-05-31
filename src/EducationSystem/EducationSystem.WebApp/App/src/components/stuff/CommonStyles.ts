import {Theme} from '@material-ui/core'

export const important = ' !important'

export const onHover = (styles: any) => ({'&:hover': styles})

export const onMobile = (theme: Theme) => (styles: any) => ({
  [theme.breakpoints.down('sm')]: styles
})

export const getBaseContentPadding = (theme: Theme) => theme.spacing.unit * 5

export const headerStyles = (theme: Theme) => ({
  header: {
    backgroundColor: theme.palette.primary.main,
    padding: `${theme.spacing.unit}px ${theme.spacing.unit * 3}px !important`,
    color: theme.palette.primary.contrastText
  },
  headerText: {
    color: theme.palette.primary.contrastText
  }
})

export const breadcrumbsStyles = (theme: Theme) => ({
  ...headerStyles(theme),
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
  }
})