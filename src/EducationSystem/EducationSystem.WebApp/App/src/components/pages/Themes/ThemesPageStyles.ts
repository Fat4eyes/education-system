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
  }
})

export default ThemesPageStyles