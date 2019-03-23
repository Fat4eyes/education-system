import React from 'react'
import {If} from '../core'
import {Typography, withStyles} from '@material-ui/core'
import VTextField from './VTextField'
import MaskedInput from 'react-text-mask'
import Input from '@material-ui/core/Input'
import InputLabel from '@material-ui/core/InputLabel'

const styles = () => ({
  root: {
    display: 'inline-flex',
    padding: 0,
    position: 'relative',
    flexDirection: 'column',
    verticalAlign: 'top',
    marginTop: 16,
    marginBottom: 8,
    width: '100%'
  },
  label: {
    fontSize: '0.75rem',
    height: 16
  },
  input: {
    fontSize: 1
  }
})

@withStyles(styles)
class VMaskedField extends VTextField {
  render() {
    let {onChange, id, label, required, mask, styles = {}, classes, ...rest} = this.props

    const TextMask = ({inputRef, onChange, ...rest}) =>
      <MaskedInput {...rest} mask={mask} showMask ref={ref => inputRef(ref ? ref.inputElement : null)}/>

    return <div className={classes.root}>
      <InputLabel
        htmlFor={id}
        required={!!required}
        error={!this.state.isValid}
        className={classes.label}
        style={{...styles.label}}
      >
        {label}
      </InputLabel>
      <Input
        id={id}
        onBlur={e => {
          this.handleBlur(e)
          onChange(e)
        }}
        error={!this.state.isValid}
        inputComponent={TextMask}
        style={{...styles.input}}
        {...rest}
      />
      <If condition={!this.state.isValid}>
        <Typography color='error' style={{...styles.errors}}>
          {this.state.error}
        </Typography>
      </If>
    </div>
  }
}

export default VMaskedField