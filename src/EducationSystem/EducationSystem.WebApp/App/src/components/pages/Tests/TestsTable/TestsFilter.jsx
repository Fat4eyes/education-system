import React, {Component} from 'react'
import {Typography} from '@material-ui/core'
import FormControl from '@material-ui/core/FormControl'
import InputLabel from '@material-ui/core/InputLabel'
import Select from '@material-ui/core/Select'
import FormControlLabel from '@material-ui/core/FormControlLabel'
import Switch from '@material-ui/core/Switch'
import MenuItem from '@material-ui/core/MenuItem'
import PropTypes from 'prop-types'
import withStyles from '@material-ui/core/styles/withStyles'
import withWidth, {isWidthDown} from '@material-ui/core/withWidth'
import {Search} from '../../../core'
import Grid from '@material-ui/core/Grid'
import Collapse from '@material-ui/core/Collapse'
import Paper from '@material-ui/core/Paper'

class TestsFilter extends Component {
  constructor(props) {
    super(props)

    this.state = {Open: !isWidthDown('md', this.props.width)}
  }

  handleCollapse = () => this.setState({Open: !this.state.Open})

  render() {
    let {classes, Disciplines, DisciplineId, Name, IsActive, handleInput, handleSearch} = this.props
    return <Paper className={classes.root}>
      <Grid item xs={12} container justify='center' spacing={8}>
        <Grid item xs={12} container wrap='nowrap' zeroMinWidth justify='center'
              onClick={this.handleCollapse} className={classes.cursor}>
          <Typography noWrap variant='subtitle1'>Фильтр</Typography>
        </Grid>
        <Collapse in={this.state.Open} className={classes.collapse}>
          <Grid item container xs={12}>
            <Grid item xs={12} className={classes.control}>
              <Search name='Name' value={Name} onChange={handleSearch}/>
            </Grid>
            <Grid item container xs={12} wrap='nowrap' zeroMinWidth
                  component={FormControl} className={classes.control}>
              <InputLabel htmlFor="group">Дисциплина</InputLabel>
              <Select autoWidth
                      value={DisciplineId}
                      onChange={handleInput()}
                      inputProps={{name: 'DisciplineId', id: 'DisciplineId'}}>
                <MenuItem value={0}>Любая</MenuItem>
                {Disciplines.map(d => <MenuItem key={d.Id} value={d.Id}>{d.Abbreviation}</MenuItem>)}
              </Select>
            </Grid>
            <Grid item xs={12} component={FormControl} className={classes.control}>
              <FormControlLabel label='Только активные' control={
                <Switch checked={IsActive}
                        onChange={handleInput(true)}
                        name='IsActive'
                        color='primary'/>}/>
            </Grid>
          </Grid>
        </Collapse>
      </Grid>
    </Paper>
  }
}

TestsFilter.propTypes = {
  Disciplines: PropTypes.array.isRequired,
  DisciplineId: PropTypes.number.isRequired,
  IsActive: PropTypes.bool.isRequired,
  handleInput: PropTypes.func.isRequired,
  handleSearch: PropTypes.func.isRequired,
  Name: PropTypes.string.isRequired
}

const styles = theme => ({
  root: {
    backgroundColor: theme.palette.grey['50'],
    padding: theme.spacing.unit * 3
  },
  collapse: {
    width: '100%'
  },
  control: {
    margin: `${theme.spacing.unit}px 0`
  },
  cursor: {
    cursor: 'pointer'
  }
})

export default withStyles(styles)(withWidth()(TestsFilter))