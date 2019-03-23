import {createStyles, Grid, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'

const styles = (theme: Theme) => createStyles({
  root: {
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
}


const RowBody = ({classes, children}: IRowProps) =>
  <Grid container className={classes.root}>
    {children}
  </Grid>

export default withStyles(styles)(RowBody)