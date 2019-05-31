import {createStyles, Theme, WithStyles, withStyles} from '@material-ui/core'
import * as React from 'react'
import {FunctionComponent, PropsWithChildren, ReactNode} from 'react'
import classNames from 'classnames'
import {onMobile} from '../stuff/CommonStyles'

export const blockPaddingFactor = 4.5

const styles = (theme: Theme) => {
  const padding = theme.spacing.unit * blockPaddingFactor
  const border = '1px solid'
  return createStyles({
    root: {
      border: border,
      ...onMobile(theme)({
        borderLeft: 'none',
        borderRight: 'none',
        borderRadius: 0
      }),
      borderColor: theme.palette.grey['400'],
      borderRadius: 4,
      backgroundColor: theme.palette.grey['50'],
      boxShadow: '0px 0px 4px rgba(0, 0, 0, 0.1)'
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
    },
    topBot: {
      padding: `${padding}px 0`
    }
  })
}

interface IProps extends WithStyles<typeof styles> {
  children?: ReactNode,
  partial?: boolean,
  empty?: boolean,
  small?: boolean,
  topBot?: boolean
}

const Block = ({classes, children, partial = false, empty = false, small = false, topBot = false}: IProps) =>
  <div className={classNames(classes.root, {
    [classes.full]: !partial && !empty && !small,
    [classes.partial]: partial && !empty && !small,
    [classes.smallFull]: !partial && !empty && small,
    [classes.smallPartial]: partial && !empty && small,
    [classes.topBot]: topBot
  })}>
    {children}
  </div>

export default withStyles(styles)(Block)

interface IPBlock {
  top?: boolean,
  left?: boolean
}

const pBlockStyles = (theme: Theme) => createStyles({
  top: {
    paddingTop: theme.spacing.unit * blockPaddingFactor,
    paddingBottom: theme.spacing.unit * blockPaddingFactor
  },
  left: {
    paddingLeft: theme.spacing.unit * blockPaddingFactor,
    paddingRight: theme.spacing.unit * blockPaddingFactor
  },
  root: {
    width: '100%'
  }
})

export const PBlock = withStyles(pBlockStyles)(
  ({top = false, left = false, children, classes}: PropsWithChildren<IPBlock & WithStyles<typeof pBlockStyles>>) =>
    <div className={classNames(classes.root, {
      [classes.top]: top,
      [classes.left]: left
    })}>
      {children}
    </div>
) as FunctionComponent<IPBlock>