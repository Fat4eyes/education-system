import React, {Component} from 'react'
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider'
import {withSnackbar} from 'notistack'
import {Grid, Typography, withStyles} from '@material-ui/core'
import {Handlers} from '../../../helpers'
import styles from './styles'
import Block from '../../Blocks/Block'
import withWidth from '@material-ui/core/withWidth/withWidth'
import {MtBlock} from '../../stuff/Margin'

@withWidth()
@withSnackbar
@withStyles(styles)
@withAuthenticated
class Account extends Component {
  render() {
    let {classes, auth: {User}} = this.props

    return <Grid container justify='center' className={classes.grid}>
      <Grid item xs={12} container>
        <Grid item xs={12}>
          <Block>
            <Grid container alignItems='center' wrap='nowrap' justify='center'>
              <Typography variant='h5' inline color='inherit' className={classes.fullNameText}>
                {Handlers.getFullName(User)}
              </Typography>
            </Grid>
          </Block>
        </Grid>
      </Grid>
      <MtBlock value={5}/>
      <Grid item xs={12} container>
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
            {User.Group && <>
              <Grid item xs={12} className={classes.mt2Unit}/>
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