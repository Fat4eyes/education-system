import React from 'react'
import Grid from '@material-ui/core/Grid'
import TestsTable from './TestsTable/TestsTable'

const Tests = () => <Grid container justify='space-around'>
  <Grid item xs={12}>
    <TestsTable/>
  </Grid>
</Grid>

export default Tests