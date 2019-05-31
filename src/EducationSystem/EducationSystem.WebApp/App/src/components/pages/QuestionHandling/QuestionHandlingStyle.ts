import {createStyles, Theme} from '@material-ui/core'
import {headerStyles, important, onHover} from '../../stuff/CommonStyles'

const QuestionHandlingStyle = (theme: Theme) => createStyles({
  ...headerStyles(theme),
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50']
  },
  image: {
    maxHeight: 117,
    width: '100%',
  },
  openButton: {
    padding: 0 + important,
    width: '100%',
    backgroundColor: theme.palette.grey['300'],
    ...onHover({
      backgroundColor: theme.palette.grey['400']
    })
  },
  chip: {
    margin: '6px 12px'
  },
  inputsBlock: {
    margin: 0 + important,
    '&>div': {
      paddingTop: 0 + important,
      paddingBottom: 0 + important
    }
  },
  addPhotoButton: {
    
  }
})

export default QuestionHandlingStyle