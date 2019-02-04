import React, {Component} from 'react'
import {
  Avatar,
  Button,
  Checkbox,
  CircularProgress,
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
import {withSnackbar} from "notistack";
import {ValidateAuthModel} from "../common";
import history from "../../../helpers/history";

@withSnackbar
@withStyles(styles)
class AuthModal extends Component {
  state = {
    email: '',
    password: '',
    isLoading: false
  };

  handleInput = ({target: {name, value}}) => this.setState({[name]: value});

  handleSubmit = (handleClose, signInHandler) => async () => {
    const authModel = {
      Email: this.state.email,
      Password: this.state.password
    };

    try {
      ValidateAuthModel(authModel);
      this.setState({isLoading: true});
      let result = await signInHandler(authModel);
      this.setState({isLoading: false});
      if (result && typeof handleClose === 'function') handleClose();
    } catch (e) {
      this.props.enqueueSnackbar(e, {
        variant: 'error',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right',
        },
      });
      this.setState({isLoading: false});
    }
  };

  render() {
    let {
      classes, 
      handleClose,
      handleReject,
      open,
      signIn
    } = this.props;
    handleReject = handleReject || handleClose;

    return <Modal open={open} onClose={handleReject}>
      <div className={classes.root}>
        <div className={classes.header}>
          <Avatar className={classes.avatar}><LockOutlinedIcon/></Avatar>
          <Typography component='h1' variant='h5'>Вход</Typography>
        </div>
        <div className={classes.form}>
          {this.state.isLoading
            ? <CircularProgress className={classes.progress} size={50} thickness={5}/>
            : <>
              <FormControl margin='normal' required fullWidth>
                <InputLabel htmlFor='email'>Имя ящика</InputLabel>
                <Input name='email' autoFocus onChange={this.handleInput} value={this.state.email}/>
              </FormControl>
              <FormControl margin='normal' required fullWidth>
                <InputLabel htmlFor='password'>Пароль</InputLabel>
                <Input name='password' type='password' onChange={this.handleInput} value={this.state.password}/>
              </FormControl>
            </>
          }
          <Button onClick={this.handleSubmit(handleClose, signIn)}
                  type='submit'
                  fullWidth
                  variant='contained'
                  color='primary'
                  disabled={this.state.isLoading}
                  className={classes.submit}>
            Войти
          </Button>
        </div>
      </div>
    </Modal>;
  }
}

export default AuthModal