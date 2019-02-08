import React, {Component} from 'react'
import Grid from '@material-ui/core/Grid'
import {
  Avatar,
  Button,
  CircularProgress,
  FormControl,
  Input,
  InputLabel,
  Typography,
  withStyles
} from '@material-ui/core'
import LockOutlinedIcon from '@material-ui/icons/LockOutlined'
import styles from './SignInStyles'
import If from '../../If'
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider'
import {withSnackbar} from 'notistack'
import {Redirect} from 'react-router-dom'

@withStyles(styles)
@withSnackbar
@withAuthenticated
class SignIn extends Component {
  state = {
    IsLoading: false,
    Email: '',
    Password: ''
  }

  handleInput = ({target: {name, value}}) => this.setState({[name]: value})
  handleSubmit = async () => {
    const authModel = {
      Email: this.state.Email,
      Password: this.state.Password
    }

    try {
      this.setState({IsLoading: true})
      await this.props.auth.signIn(authModel)
    } catch (e) {
      this.props.enqueueSnackbar(e, {
        variant: 'error',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })
      this.setState({IsLoading: false})
    }
  }


  render() {
    let {classes, auth: {checkAuth}} = this.props
    let {from} = this.props.location.state || {from: {pathname: '/'}}

    if (checkAuth()) {
      return <Redirect to={from}/>
    }

    return <Grid container justify='center' className={classes.root}>
      <Grid container item xs={10} sm={9} md={6} lg={4} justify='center' className={classes.form} direction='column'
            spacing={16}>
        <Grid item xs>
          <Avatar className={classes.marginAuto}><LockOutlinedIcon/></Avatar>
        </Grid>
        <Grid item xs>
          <Typography variant='h5' className={classes.label}>Вход</Typography>
        </Grid>
        <If condition={this.state.IsLoading}>
          <Grid item xs>
            <CircularProgress size={50} thickness={5}/>
          </Grid>
        </If>
        <If condition={!this.state.IsLoading}>
          <Grid item xs>
            <FormControl margin='normal' fullWidth required>
              <InputLabel htmlFor='Email'>Электронная почта</InputLabel>
              <Input name='Email' autoFocus onChange={this.handleInput} value={this.state.Email}/>
            </FormControl>
            <FormControl margin='normal' fullWidth required>
              <InputLabel htmlFor='Password'>Пароль</InputLabel>
              <Input name='Password' type='password' onChange={this.handleInput} value={this.state.Password}/>
            </FormControl>
          </Grid>
        </If>
        <Grid item xs>
          <Button onClick={this.handleSubmit} type='submit' fullWidth variant='contained' color='primary'
                  disabled={this.state.IsLoading}>
            Войти
          </Button>
        </Grid>
      </Grid>
    </Grid>
  }
}

export default SignIn