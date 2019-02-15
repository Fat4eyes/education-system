import React from 'react'
import {Typography} from '@material-ui/core'
import ExpansionPanel from '@material-ui/core/ExpansionPanel'
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary'
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails'
import FormControl from '@material-ui/core/FormControl'
import InputLabel from '@material-ui/core/InputLabel'
import Select from '@material-ui/core/Select'
import FormControlLabel from '@material-ui/core/FormControlLabel'
import Switch from '@material-ui/core/Switch'
import MenuItem from '@material-ui/core/MenuItem'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import PropTypes from 'prop-types'
import withStyles from '@material-ui/core/styles/withStyles'
import withWidth, {isWidthDown} from '@material-ui/core/withWidth'
import {Search} from '../../../core'

const TestsFilter = ({classes, Disciplines, DisciplineId, Name, IsActive, handleInput, handleSearch, width}) => {
  let isXs = isWidthDown('xs', width)
  return <ExpansionPanel defaultExpanded={!isXs} className={classes.expansionPanel}>
    <ExpansionPanelSummary expandIcon={<ExpandMoreIcon/>}>
      <Typography>Фильтр</Typography>
    </ExpansionPanelSummary>
    <ExpansionPanelDetails className={classes.expansionPanelDetails}>
      <form autoComplete='off' className={classes.form}>
        <FormControl className={classes.formControl}>
          <Search name='Name' value={Name} onChange={handleSearch}/>
        </FormControl>
        <FormControl className={classes.formControl}>
          <InputLabel htmlFor="group">Дисциплина</InputLabel>
          <Select value={DisciplineId}
                  onChange={handleInput()}
                  inputProps={{name: 'DisciplineId', id: 'DisciplineId'}}>
            <MenuItem value={0}>Любая</MenuItem>
            {Disciplines.map(d => <MenuItem key={d.Id} value={d.Id}>{d.Abbreviation}</MenuItem>)}
          </Select>
        </FormControl>
        <FormControl className={classes.formControl}>
          <FormControlLabel label='Только активные' control={
            <Switch checked={IsActive}
                    onChange={handleInput(true)}
                    name='IsActive'
                    color='primary'/>}/>
        </FormControl>
      </form>
    </ExpansionPanelDetails>
  </ExpansionPanel>
}

TestsFilter.propTypes = {
  Disciplines: PropTypes.array.isRequired,
  DisciplineId: PropTypes.number.isRequired,
  IsActive: PropTypes.bool.isRequired,
  handleInput: PropTypes.func.isRequired,
  handleSearch: PropTypes.func.isRequired,
  Name: PropTypes.string.isRequired,
}

const styles = theme => ({
  form: {
    minWidth: '100%'
  },
  formControl: {
    margin: theme.spacing.unit,
    minWidth: `calc(100% - ${theme.spacing.unit * 2}px)`
  },
  expansionPanel: {
    backgroundColor: theme.palette.grey['50']
  },
  expansionPanelDetails: {
    padding: `0 ${theme.spacing.unit * 2}px ${theme.spacing.unit * 3}px !important`
  }
})

export default withStyles(styles)(withWidth()(TestsFilter))