import React from 'react'
import {NavLink} from 'react-router-dom'
import {withTheme} from '@material-ui/core/styles'

const SimpleLink = props => {
  return <NavLink
    {...props}
    exact
    activeStyle={{
      color: props.theme.palette.secondary.light
    }}
  />
}

export default withTheme()(SimpleLink)