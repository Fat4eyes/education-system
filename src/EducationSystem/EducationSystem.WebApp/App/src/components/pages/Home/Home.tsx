import React from 'react'
import Grid from '@material-ui/core/Grid'
import MaterialEditor from '../../MaterialEditor/MaterialEditor'

class Home extends React.Component {
  render() {
    return <Grid container spacing={8}>
      <MaterialEditor/>
    </Grid>
  }
}

export default Home