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
import {If} from '../../core'
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider'
import {withSnackbar} from 'notistack'
import {Redirect} from 'react-router-dom'

@withStyles(styles)
@withSnackbar
@withAuthenticated
class SignIn extends Component {
  constructor(props) {
    super(props)

    this.state = {
      IsLoading: false,
      Redirect: true,
      Email: '',
      Password: ''
    }
  }

  handleInput = ({target: {name, value}}) => this.setState({[name]: value})
  handleSubmit = async () => {
    this.setState({IsLoading: true, Redirect: false})
    const result = await this.props.auth.signIn({Email: this.state.Email.trim(), Password: this.state.Password.trim()})
    this.setState({IsLoading: false, Redirect: !!result})
  }
  handleKeyDown = async ({key}) => {
    if (key === 'Enter') {
      await this.handleSubmit()
    }
  }
  
  render() {
    const {classes, auth: {checkAuth}} = this.props
    const {from} = this.props.location.state || {from: {pathname: '/'}}

    return <If condition={!(this.state.Redirect && checkAuth())} orElse={<Redirect to={from}/>}>
      <Grid container justify='center' className={classes.root} onKeyDown={this.handleKeyDown}>
        <Grid container item xs={10} sm={9} md={6} lg={4} justify='center' className={classes.form} direction='column'
              spacing={16}>
          <Grid item xs>
            <Avatar className={classes.marginAuto}><LockOutlinedIcon/></Avatar>
          </Grid>
          <Grid item xs>
            <Typography variant='h5' className={classes.label}>Вход</Typography>
          </Grid>
          <If condition={!this.state.IsLoading} orElse={
            <Grid item xs className={classes.progress}>
              <CircularProgress size={50} thickness={5}/>
            </Grid>
          }>
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
    </If>
  }
}

export default SignIn