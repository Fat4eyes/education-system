import React, {Component} from 'react'
import {
  Avatar,
  Button,
  Checkbox,
  FormControl,
  FormControlLabel,
  Input,
  InputLabel,
  Modal,
  Typography,
  withStyles
} from '@material-ui/core';
import styles from './styles'
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import {withAuthenticated} from '../../../../providers/AuthProvider/AuthProvider'
import {ValidateAuthModel} from '../../../../providers/AuthProvider/common'
import {withSnackbar} from "notistack";

@withAuthenticated
@withStyles(styles)
@withSnackbar
class SingIn extends Component {
  state = {
    email: '',
    password: '',
    remember: false
  };

  handleInput = ({target: {name, value}}) => this.setState({[name]: value});
  handleCheckbox = ({target: {checked}}) => this.setState({remember: checked});

  handleSubmit = (signInHandler) => async () => {
    const authModel = {
      Email: this.state.email,
      Password: this.state.password,
      Remember: this.state.remember
    };

    try {
      ValidateAuthModel(authModel);
      let result = await signInHandler(authModel);
      if (result) this.props.handleClose();
    } catch (e) {
      this.props.enqueueSnackbar(e, {
        variant: 'error',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right',
        },
      });
    }
  };

  render() {
    const {classes, handleClose, open, auth: {signIn}} = this.props;
    let {handleReject} = this.props;
    handleReject = handleReject || handleClose;

    const SubmitButton = ({signInHandler}) =>
      <Button onClick={this.handleSubmit(signInHandler)} type='submit' fullWidth
              variant='contained' color='primary' className={classes.submit}>
        Войти
      </Button>;

    return <Modal open={open} onClose={handleReject}>
      <div className={classes.root}>
        <Avatar className={classes.avatar}><LockOutlinedIcon/></Avatar>
        <Typography component='h1' variant='h5'>Вход</Typography>
        <div className={classes.form}>
          <FormControl margin='normal' required fullWidth>
            <InputLabel htmlFor='email'>Имя ящика</InputLabel>
            <Input id='email' name='email' autoComplete='email' autoFocus onChange={this.handleInput}/>
          </FormControl>
          <FormControl margin='normal' required fullWidth>
            <InputLabel htmlFor='password'>Пароль</InputLabel>
            <Input name='password' type='password' id='password' autoComplete='current-password'
                   onChange={this.handleInput}/>
          </FormControl>
          <FormControlLabel control={<Checkbox onClick={this.handleCheckbox} color='primary'/>} label='Запомнить'/>
          <SubmitButton classes={classes} signInHandler={signIn}/>
        </div>
      </div>
    </Modal>;
  }
}

export default SingIn