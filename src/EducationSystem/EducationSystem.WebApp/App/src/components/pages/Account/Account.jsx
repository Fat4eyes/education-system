import React, {Component} from 'react'
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider'
import {withSnackbar} from 'notistack'
import {Grid, Tooltip, Typography, withStyles} from '@material-ui/core'
import AccountIcon from '@material-ui/icons/AccountCircleOutlined'
import CheckIcon from '@material-ui/icons/Check'
import {Handlers} from '../../../helpers'
import styles from './styles'
import Block from '../../Blocks/Block'
import {isWidthDown} from '@material-ui/core/withWidth'
import withWidth from '@material-ui/core/withWidth/withWidth'

@withWidth()
@withSnackbar
@withStyles(styles)
@withAuthenticated
class Account extends Component {
  render() {
    let {classes, auth: {User}, width} = this.props
    let isXs = isWidthDown('xs', width)

    return <Grid container justify='center' spacing={24} className={classes.grid}>
      <Grid item xl={7} sm={10} xs={12} container spacing={24}>
        <Grid item xs>
          <Block>
            <Grid container alignItems='center' wrap="nowrap" spacing={16}>
              <Grid item>
                <AccountIcon color='primary' fontSize='large'/>
              </Grid>
              <Grid item xs>
                <Typography variant='h6' inline color='inherit'>
                  {Handlers.getFullName(User)}
                </Typography>
              </Grid>
            </Grid>
          </Block>
        </Grid>
        {
          User.Active &&
          <Grid item xs={isXs}>
            <Block>
              {
                isXs
                  ? <Grid container alignItems='center' wrap="nowrap" spacing={16}>
                    <Grid item>
                      <CheckIcon color='primary' fontSize='large'/>
                    </Grid>
                    <Grid item xs>
                      <Typography inline color='inherit'>
                        Учетная запись подтверждена
                      </Typography>
                    </Grid>
                  </Grid>
                  : <Tooltip title='Учетная запись подтверждена'>
                    <CheckIcon color='primary' fontSize='large'/>
                  </Tooltip>
              }
            </Block>
          </Grid>
        }
      </Grid>
      <Grid item xl={7} sm={10} xs={12} container spacing={24}>
        <Grid item xs={12}>
          <Block partial>
            <Grid item xs={12} className={classes.header}>
              <Typography className={classes.text} align='center' noWrap color='inherit'>
                Электронная почта
              </Typography>
            </Grid>
            <Grid item xs={12} className={classes.body}>
              <Typography align='center' noWrap color='inherit'>
                {User.Email}
              </Typography>
            </Grid>
            <Grid item xs={12} className={classes.mt2Unit}/>
            <Grid item xs={12} className={classes.header}>
              <Typography className={classes.text} align='center' noWrap color='inherit'>
                Электронная почта
              </Typography>
            </Grid>
            <Grid item xs={12} className={classes.body}>
              <Typography align='center' noWrap color='inherit'>
                {User.Email}
              </Typography>
            </Grid>
            {User.Group && <>
              <Grid item xs={12} className={classes.header}>
                <Typography className={classes.text} align='center' noWrap color='inherit'>
                  Группа
                </Typography>
              </Grid>
              <Grid item xs={12} className={classes.body}>
                <Typography align='center' noWrap color='inherit'>
                  {User.Group.Name}
                </Typography>
              </Grid>
            </>}
          </Block>
        </Grid>
      </Grid>
    </Grid>
  }
}

export default Account