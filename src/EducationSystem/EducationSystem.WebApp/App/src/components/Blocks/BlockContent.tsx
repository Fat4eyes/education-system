import {createStyles, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {ReactNode} from 'react'
import classNames from 'classnames'

interface IRowProps {
  children?: ReactNode,
  top?: boolean,
  bottom?: boolean
}

const styles = (theme: Theme) => createStyles({
  top: {
    marginTop: theme.spacing.unit * 3
  },
  bottom: {
    marginBottom: theme.spacing.unit * 3
  }
})

const BlockContent = ({classes, children, top = false, bottom = false}: IRowProps & WithStyles<typeof styles>) =>
  <div className={classNames({
    [classes.top]: top,
    [classes.bottom]: bottom
  })}>
    {children}
  </div>

export default withStyles(styles)(BlockContent)