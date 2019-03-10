import {createStyles, Theme} from '@material-ui/core'

const HandleTestStyles = (theme: Theme) => createStyles({
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50']
  },
  stepper: {
    backgroundColor: theme.palette.grey['50']
  },
})

export default HandleTestStyles