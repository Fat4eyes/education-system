import {createStyles, Paper, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'
import classNames from 'classnames'

const styles = (theme: Theme) => {
  const padding = theme.spacing.unit * 4.5
  
  return createStyles({
    partial: {
      padding: `${padding}px 0`,
      '&>div': {
        padding: `0 ${padding}px`
      }
    },
    full: {
      padding: padding
    }
  })
}

interface IProps extends WithStyles<typeof styles> {
  children?: ReactNode,
  partial?: boolean
}

const Block = ({classes, children, partial = false}: IProps) => 
  <Paper className={classNames({
    [classes.full]: !partial,
    [classes.partial]: partial
  })}>
    {children}
  </Paper>

export default withStyles(styles)(Block)