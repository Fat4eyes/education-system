import React from 'react'
import {Typography} from '@material-ui/core'

const Text = ({children, fontSize = 20}) => <Typography component='p' style={{fontSize: `${fontSize}px`}}>
  {children}
</Typography>

export default Text