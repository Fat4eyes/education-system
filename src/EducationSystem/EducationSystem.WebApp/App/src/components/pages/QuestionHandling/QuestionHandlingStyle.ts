import {createStyles, Theme} from '@material-ui/core'

const QuestionHandlingStyle = (theme: Theme) => createStyles({
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50']
  }
})

export default QuestionHandlingStyle