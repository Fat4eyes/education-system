import {createStyles, Grid, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {FunctionComponent, ReactNode} from 'react'

const styles = (theme: Theme) => {
  return createStyles({
    header: {
      backgroundColor: theme.palette.primary.main,
      padding: `${theme.spacing.unit}px ${theme.spacing.unit * 3}px !important`,
      color: theme.palette.primary.contrastText,
      '& span': {
        color: theme.palette.primary.contrastText
      }
    }
  })
}

interface IProps extends WithStyles<typeof styles> {
  children?: ReactNode,
}

const BlockHeader = ({classes, children}: IProps) =>
  <Grid item xs={12} className={classes.header} container zeroMinWidth wrap='nowrap'>
    {children}
  </Grid>

export default withStyles(styles)(BlockHeader) as FunctionComponent<{}>