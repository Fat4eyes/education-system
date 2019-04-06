import React from 'react'
import Grid from '@material-ui/core/Grid'
import MaterialSelect from '../../Table/MaterialSelect'

class Home extends React.Component {
  render() {
    return <Grid container spacing={8}>
      <MaterialSelect/>
    </Grid>
  }
}

export default Home