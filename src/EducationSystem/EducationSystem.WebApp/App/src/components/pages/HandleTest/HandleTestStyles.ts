import {createStyles, Theme} from '@material-ui/core'

const HandleTestStyles = (theme: Theme) => createStyles({
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50']
  },
  stepper: {
    backgroundColor: theme.palette.grey['50']
  },
  buttonBlock: {
    marginTop: theme.spacing.unit * 3,
    
    '& button': {
      marginRight: theme.spacing.unit * 3
    }
  }
})

export default HandleTestStyles