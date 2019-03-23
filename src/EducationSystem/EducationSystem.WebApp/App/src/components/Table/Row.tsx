import {createStyles, Grid, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'
import RowBody from './RowBody'
import RowHeader from './RowHeader'

const styles = (theme: Theme) => createStyles({
  root: {
    margin: `${theme.spacing.unit / 2}px 0`
  }
})

interface IRowProps extends WithStyles<typeof styles> {
  header?: ReactNode
  children?: ReactNode
}


const Row = ({classes, children, header}: IRowProps) =>
  <Grid container className={classes.root}>
    <RowHeader>
      {header}
    </RowHeader>
    <RowBody>
      {children}
    </RowBody>
  </Grid>

export default withStyles(styles)(Row)