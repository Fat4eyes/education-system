import React from 'react'
import Grid from '@material-ui/core/Grid'
import TestsTable from './TestsTable/TestsTable'

const Tests = () => <Grid container justify='space-around' spacing={16}>
  <Grid item xs={12} lg={11}>
    <TestsTable/>
  </Grid>
</Grid>

export default Tests