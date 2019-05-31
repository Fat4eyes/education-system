import {createStyles, Grid, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'
import classNames from 'classnames'
import {GridProps} from '@material-ui/core/Grid'

const styles = (theme: Theme) => createStyles({
  root: {
    cursor: 'pointer',
    margin: `${theme.spacing.unit / 2}px 0`,
    padding: `${theme.spacing.unit}px ${theme.spacing.unit * 2}px`,
    backgroundColor: theme.palette.grey['200'],
    '&:hover': {
      backgroundColor: theme.palette.grey['300']
    }
  },
  selected: {
    backgroundColor: theme.palette.grey['300']
  }
})

interface IRowProps extends GridProps {
  children?: ReactNode,
  selected?: boolean,

  [propName: string]: any
}


const RowHeader = ({classes, children, selected = false, ...rest}: IRowProps & WithStyles<typeof styles>) =>
  <Grid container zeroMinWidth wrap='nowrap' className={classNames(classes.root, {
    [classes.selected]: selected
  })} {...rest}>
    {children}
  </Grid>

export default withStyles(styles)(RowHeader)