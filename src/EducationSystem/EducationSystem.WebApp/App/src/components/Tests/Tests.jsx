import React, {Component} from 'react'
import Grid from "@material-ui/core/Grid";
import {Paper, Table, TableBody, TableHead, Typography, withStyles} from "@material-ui/core";
import BookmarkIcon from '@material-ui/icons/Bookmark';
import styles from './styles'
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import ExpansionPanel from "@material-ui/core/ExpansionPanel";
import ExpansionPanelSummary from "@material-ui/core/ExpansionPanelSummary";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore"
import ExpansionPanelDetails from "@material-ui/core/ExpansionPanelDetails";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Switch from "@material-ui/core/Switch";

@withStyles(styles)
class Tests extends Component {
  state = {
    select: [1, 2, 3, 4, 5, 6, 7, 8, 9],
    group: "",
    isActive: true
  };

  handleInput = isChecked => ({target: {name, value, checked}}) => 
    this.setState({[name]: (!!isChecked ? checked: value)}, console.log(this.state));

  render() {
    let {classes} = this.props;

    const GridRowWithIcon = ({Icon, text, children}) =>
      <Grid container alignItems='center' wrap="nowrap">
        <Grid item>
          <Icon color='primary' fontSize='large'/>
        </Grid>
        <Grid item xs>
          <Typography variant='h6' inline color='inherit'>
            {text}
          </Typography>
        </Grid>
        {children}
      </Grid>;

    return <Grid container justify='space-around' spacing={16}>
      <Grid item xs={12}>
        <Paper className={classes.paper}>
          <GridRowWithIcon Icon={BookmarkIcon} text={'Tests'}/>
        </Paper>
      </Grid>
      <Grid item xs={12}>
        <Grid container justify='space-around' className={classes.main} spacing={16}>
          <Grid item xs={12} lg={3}>
            <ExpansionPanel className={classes.expansionPanel}>
              <ExpansionPanelSummary expandIcon={<ExpandMoreIcon/>} className={classes.expansionPanelSummary}>
                <Typography>Filter</Typography>
              </ExpansionPanelSummary>
              <ExpansionPanelDetails className={classes.expansionPanelDetails}>
                <form autoComplete='off' className={classes.form}>
                  <FormControl className={classes.formControl}>
                    <InputLabel htmlFor="group">Group</InputLabel>
                    <Select value={this.state.group} onChange={this.handleInput()} inputProps={{name:'group', id: 'group'}}>
                      <MenuItem value=""><em>None</em></MenuItem>
                      {this.state.select.map(g => <MenuItem key={g} value={g}>{g}</MenuItem>)}
                    </Select>
                  </FormControl>
                  <FormControl className={classes.formControl}>
                    <InputLabel htmlFor="group">Group</InputLabel>
                    <Select value={this.state.group} onChange={this.handleInput()} inputProps={{name:'group', id: 'group'}}>
                      <MenuItem value=""><em>None</em></MenuItem>
                      {this.state.select.map(g => <MenuItem key={g} value={g}>{g}</MenuItem>)}
                    </Select>
                  </FormControl>
                  <FormControl className={classes.formControl}>
                    <InputLabel htmlFor="group">Group</InputLabel>
                    <Select value={this.state.group} onChange={this.handleInput()} inputProps={{name:'group', id: 'group'}}>
                      <MenuItem value=""><em>None</em></MenuItem>
                      {this.state.select.map(g => <MenuItem key={g} value={g}>{g}</MenuItem>)}
                    </Select>
                  </FormControl>
                  <FormControl className={classes.formControl}>
                    <FormControlLabel
                      control={
                        <Switch
                          checked={this.state.isActive}
                          onChange={this.handleInput(true)}
                          name="isActive"
                        />
                      }
                      label='dfff'
                    />
                  </FormControl>
                </form>
              </ExpansionPanelDetails>
            </ExpansionPanel>
          </Grid>
          <Grid item xs={12} lg={9}>
            <Paper className={classes.paper}>
              <Table>
                <TableHead/>
                <TableBody>
                  
                </TableBody>
              </Table>
            </Paper>
          </Grid>
        </Grid>
      </Grid>
    </Grid>;
  }
}

export default Tests