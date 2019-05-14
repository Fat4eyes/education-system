import {createStyles, Theme} from '@material-ui/core'
import {breadcrumbsStyles} from '../../stuff/CommonStyles'

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