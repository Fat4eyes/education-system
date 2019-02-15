import React, {Component} from 'react'
import Grid from '@material-ui/core/Grid'
import TestsTable from './TestsTable/TestsTable'

class Tests extends Component {
  constructor(props) {
    super(props)
  }

  render() {
    return <Grid container justify='space-around' spacing={16}>
      <Grid item xs={12} lg={11}>
        <TestsTable/>
      </Grid>
    </Grid>
  }
}

export default Tests