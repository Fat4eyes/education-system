import {createStyles, Theme} from '@material-ui/core'
import {headerStyles, important, onHover} from '../../stuff/CommonStyles'

const MaterialStyles = (theme: Theme) => createStyles({
  ...headerStyles(theme),
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50']
  },
  image: {
    height: 100
  }
})

export default MaterialStyles