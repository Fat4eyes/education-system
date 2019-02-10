import React, {Component} from 'react'
import Grid from '@material-ui/core/Grid'
import {Paper, Typography, withStyles} from '@material-ui/core'
import BookmarkIcon from '@material-ui/icons/Bookmark'
import styles from './styles'
import TestsTable from './TestsTable/TestsTable'

@withStyles(styles)
class Tests extends Component {
  constructor(props) {
    super(props)
  }

  render() {
    let {classes} = this.props

    return <Grid container justify='space-around' spacing={16}>
      <Grid item xs={12}>
        <Paper className={classes.paper}>
          <Grid container alignItems='center' wrap="nowrap">
            <Grid item>
              <BookmarkIcon color='primary' fontSize='large'/>
            </Grid>
            <Grid item xs>
              <Typography variant='h6' inline color='inherit'>
                {'Тесты'}
              </Typography>
            </Grid>
          </Grid>
        </Paper>
      </Grid>
      <Grid item xs={12}>
        <TestsTable/>
      </Grid>
    </Grid>
  }
}

export default Tests