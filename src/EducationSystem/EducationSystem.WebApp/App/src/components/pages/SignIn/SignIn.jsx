import React, {Component} from 'react'
import Grid from '@material-ui/core/Grid'
import {Avatar, Button, FormControl, Input, InputLabel, Typography, withStyles} from '@material-ui/core'
import LockOutlinedIcon from '@material-ui/icons/LockOutlined'
import styles from './SignInStyles'
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
      Email: '',
      Password: ''
    }
  }

  handleInput = ({target: {name, value}}) => this.setState({[name]: value.trim()})
  handleSubmit = () => this.props.auth.signIn({...this.state})
  handleKeyDown = async ({key}) => key === 'Enter' && await this.handleSubmit()

  render() {
    const {classes} = this.props
    if (this.props.auth.checkAuth()) return <Redirect to={this.props.location.state || '/'}/>

    return <Grid container justify='center' className={classes.root} onKeyDown={this.handleKeyDown}>
      <Grid container item xs={10} sm={9} md={6} lg={4} justify='center' className={classes.form} direction='column'>
        <Grid item xs>
          <Avatar className={classes.marginAuto}><LockOutlinedIcon/></Avatar>
        </Grid>
        <Grid item xs>
          <Typography variant='h5' className={classes.label}>Вход</Typography>
        </Grid>
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
        <Grid item xs>
          <Button onClick={this.handleSubmit} type='submit' fullWidth variant='contained' color='primary'>
            Войти
          </Button>
        </Grid>
      </Grid>
    </Grid>
  }
}

export default SignIn