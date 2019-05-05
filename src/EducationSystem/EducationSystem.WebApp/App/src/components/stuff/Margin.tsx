import * as React from 'react'
import {FunctionComponent} from 'react'
import {WithTheme, withTheme} from '@material-ui/core/styles'
import {Grid} from '@material-ui/core'

interface IMProps {
  value?: number
}

export const MtBlock = withTheme()(
  ({value = 1, theme}: WithTheme & IMProps) =>
    <Grid item xs={12} style={{marginTop: theme.spacing.unit * value}}/>
) as FunctionComponent<IMProps>

export const MrBlock = withTheme()(
  ({value = 1, theme}: WithTheme & IMProps) =>
    <Grid item style={{marginRight: theme.spacing.unit * value}}/>
) as FunctionComponent<IMProps>
