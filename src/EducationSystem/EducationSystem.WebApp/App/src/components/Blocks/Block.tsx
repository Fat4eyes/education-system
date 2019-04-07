import {createStyles, Paper, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'
import classNames from 'classnames'

const styles = (theme: Theme) => {
  const padding = theme.spacing.unit * 4.5
  
  return createStyles({
    root: {
      border: '1px solid',
      borderColor: theme.palette.grey['400'],
      borderRadius: 4,
      backgroundColor: theme.palette.grey['100'],
      boxShadow: 'inset 0px 0px 3px rgba(0, 0, 0, 0.1)'
    },
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
  <div className={classNames(classes.root, {
    [classes.full]: !partial,
    [classes.partial]: partial
  })}>
    {children}
  </div>

export default withStyles(styles)(Block)