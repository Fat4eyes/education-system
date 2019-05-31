import * as React from 'react'
import {FunctionComponent, PropsWithChildren, useState} from 'react'
import {createStyles, Theme, withStyles, WithStyles, Button as BaseButton} from '@material-ui/core'
import {ButtonProps} from '@material-ui/core/Button'
import classNames from 'classnames'

const styles = (theme: Theme) => {
  const base = {
    border: 0,
    borderRadius: 3,
    padding: `${theme.spacing.unit * 0.6}px ${theme.spacing.unit * 4}px`,
    color: theme.palette.common.white,
  }
  
  return createStyles({
    orange: {
      background: 'linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)',
      //boxShadow: '0 3px 5px 2px rgba(255, 105, 135, .3)',
      ...base
    },
    blue: {
      background: 'linear-gradient(45deg, #2196F3 30%, #21CBF3 90%)',
      //boxShadow: '0 3px 5px 2px rgba(33, 203, 243, .3)',
      ...base
    }
  })
}

interface IProps extends ButtonProps {
  mainColor?: 'orange' | 'blue' 
}

type TProps = IProps & WithStyles<typeof styles>

const Button: FunctionComponent<PropsWithChildren<TProps>> = ({classes, children, mainColor = 'blue', className, ...props}) => 
  <BaseButton className={classNames({
    [classes.orange]: mainColor === 'orange',
    [classes.blue]: mainColor === 'blue',
  }, className)} {...props}>{children}</BaseButton>

export default withStyles(styles)(Button) as FunctionComponent<PropsWithChildren<IProps>>