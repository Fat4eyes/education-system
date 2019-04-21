import {createStyles, Paper, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'
import classNames from 'classnames'

export const blockPaddingFactor = 4.5

const styles = (theme: Theme) => {
  const padding = theme.spacing.unit * blockPaddingFactor
  
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
    },
    smallPartial: {
      padding: `${padding / 2}px 0`,
      '&>div': {
        padding: `0 ${padding / 2}px`
      }
    },
    smallFull: {
      padding: padding / 2
    }
  })
}

interface IProps extends WithStyles<typeof styles> {
  children?: ReactNode,
  partial?: boolean,
  empty?: boolean,
  small?: boolean
}

const Block = ({classes, children, partial = false, empty = false, small = false}: IProps) => 
  <div className={classNames(classes.root, {
    [classes.full]: !partial && !empty && !small,
    [classes.partial]: partial && !empty && !small,
    [classes.smallFull]: !partial && !empty && small,
    [classes.smallPartial]: partial && !empty && small
  })}>
    {children}
  </div>

export default withStyles(styles)(Block)