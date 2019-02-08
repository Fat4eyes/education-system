import React, {Component} from 'react'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider';
import {withSnackbar} from 'notistack';
import styles from './styles'
import {Divider, Grid, Paper, Typography, Tooltip, withStyles} from '@material-ui/core';
import AccountIcon from '@material-ui/icons/AccountCircleOutlined';
import ListIcon from '@material-ui/icons/InfoOutlined';
import CheckIcon from '@material-ui/icons/Check';

@withSnackbar
@withStyles(styles)
@withAuthenticated
class Account extends Component {
  render() {
    let {classes, auth: {User, getFullName}} = this.props;
    
    const GridRow = ({name, value}) =>
      <Grid item container spacing={16} justify='space-around'>
        <Grid item xl={4} xs={6}>
          <Typography className={classes.text} align='right' color='inherit' noWrap>
            {name}:
          </Typography>
        </Grid>
        <Grid item xl={4} xs={6}>
          <Typography className={classes.text} align='left' color='inherit' noWrap>
            {value}
          </Typography>
        </Grid>
      </Grid>;

    const GridRowWithIcon = ({Icon, text, children}) =>
      <Grid container alignItems='center' wrap="nowrap" spacing={16}>
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

    return <div className={classes.root}>
      <Grid container justify='center' spacing={24} className={classes.grid}>
        <Grid item xl={7} sm={10} xs={12}>
          <Paper className={classes.paper} elevation={1}>
            <GridRowWithIcon Icon={AccountIcon} text={getFullName()} children={
              User.Active && <Grid item>
                <Tooltip title='Учетная запись подтверждена'>
                  <CheckIcon/>
                </Tooltip>
              </Grid>
            }/>
          </Paper>
        </Grid>
        <Grid item xl={7} sm={10} xs={12}>
          <Paper className={classes.paper} elevation={1}>
            <GridRowWithIcon Icon={ListIcon} text='Основная информация'/>
            <Divider className={classes.divider}/>
            <GridRow name='Электронная почта' value={User.Email}/>
            {User.Group && <GridRow name='Группа' value={User.Group.Name}/>}
          </Paper>
        </Grid>
      </Grid>
    </div>;
  }
}

export default Account