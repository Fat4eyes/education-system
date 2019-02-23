import React from 'react'
import {If} from '../core'
import {Typography} from '@material-ui/core'
import VTextField from './VTextField'
import MaskedInput from 'react-text-mask'
import Input from '@material-ui/core/Input'
import InputLabel from '@material-ui/core/InputLabel'

class VMaskedField extends VTextField {
  render() {
    let {onChange, id, label, required, mask, styles, ...rest} = this.props

    const TextMask = ({inputRef, onChange, ...rest}) => 
      <MaskedInput {...rest} mask={mask} showMask ref={ref => inputRef(ref ? ref.inputElement : null)}/>
      
    return <>
      <InputLabel 
        htmlFor={id} 
        required={!!required}
        error={!this.state.isValid}
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
    </>
  }
}

export default VMaskedField