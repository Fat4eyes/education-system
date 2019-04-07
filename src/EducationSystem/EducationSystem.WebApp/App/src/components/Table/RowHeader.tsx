import {createStyles, Grid, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'
import classNames from 'classnames'

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

interface IRowProps extends WithStyles<typeof styles> {
  children?: ReactNode,
  selected?: boolean,

  [propName: string]: any
}


const RowHeader = ({classes, children, selected = false, ...rest}: IRowProps) =>
  <Grid container className={classNames(classes.root, {
    [classes.selected]: selected
  })} {...rest}>
    {children}
  </Grid>

export default withStyles(styles)(RowHeader)