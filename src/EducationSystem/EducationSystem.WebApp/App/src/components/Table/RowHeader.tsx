import {createStyles, Grid, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'

const styles = (theme: Theme) => createStyles({
  root: {
    cursor: 'pointer',
    margin: `${theme.spacing.unit / 2}px 0`,
    padding: `${theme.spacing.unit}px ${theme.spacing.unit * 2}px`,
    backgroundColor: theme.palette.grey['200'],
    '&:hover': {
      backgroundColor: theme.palette.grey['300']
    }
  }
})

interface IRowProps extends WithStyles<typeof styles> {
  children?: ReactNode
  [propName: string]: any
}


const RowHeader = ({classes, children, ...rest}: IRowProps) =>
  <Grid container className={classes.root} {...rest}>
    {children}
  </Grid>

export default withStyles(styles)(RowHeader)