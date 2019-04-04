import {createStyles, Theme} from '@material-ui/core'

const MaterialStyles = (theme: Theme) => createStyles({
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50']
  },
  image: {
    height: 100
  }
})

export default MaterialStyles