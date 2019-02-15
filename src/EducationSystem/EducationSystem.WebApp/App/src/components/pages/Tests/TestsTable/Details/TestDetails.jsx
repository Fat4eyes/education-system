import React from 'react'
import {Grid, Typography, withStyles} from '@material-ui/core'
import ThemesTable from './ThemesTable'
import If from '../../../../stuff/If'
import PropTypes from 'prop-types'

const TestDetails = ({test, handleDetailsLoad, classes, ...props}) => {
  return <Grid container className={classes.root} justify='flex-end'>
    <Grid item xs={12}>
      {/*TODO Информация по тесту, управляющие кнопки, что-то еще?*/}
      <Typography variant='subtitle1'>
        {test.Subject}
      </Typography>
    </Grid>
    <If condition={test.IsSelected}>
      <ThemesTable TestId={test.Id} handleDetailsLoad={handleDetailsLoad}/>
    </If>
  </Grid>
}

TestDetails.propTypes = {
  test: PropTypes.shape({
    Id: PropTypes.number.isRequired,
    IsSelected: PropTypes.bool.isRequired,
  }).isRequired
}

const styles = theme => ({
  root: {
    backgroundColor: theme.palette.grey['50'],
    padding: `${theme.spacing.unit}px ${theme.spacing.unit * 2}px`
  },
  collapse: {
    width: '100%'
  }
})

export default withStyles(styles)(TestDetails)