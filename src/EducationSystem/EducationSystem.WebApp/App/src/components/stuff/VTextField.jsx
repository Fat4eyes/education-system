import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {If} from '../core'
import {FormControl, InputLabel, Typography} from '@material-ui/core'
import Input from './Input'

class VTextField extends Component {
  constructor(props) {
    super(props)

    this.state = {
      isValid: true,
      error: ''
    }
  }

  handleBlur = ({target: {value}}) => {
    const {validators: {min, max, required}, type} = this.props
    switch (type) {
      case 'duration':
        const [minutes, seconds] = value.split(':')
        const currentTimeInSeconds = Number(minutes) * 60 + Number(seconds)
        if (min && currentTimeInSeconds < min.value) {
          return this.setState({isValid: false, error: min.message})
        }
        if (max && currentTimeInSeconds > max.value) {
          return this.setState({isValid: false, error: max.message})
        }
        if (required && !currentTimeInSeconds) {
          return this.setState({isValid: false, error: ''})
        }
        break
      case 'number':
        value = Number(value)
        if (min && value < Number(min)) {
          return this.setState({isValid: false, error: min.message})
        }
        if (max && value > Number(max)) {
          return this.setState({isValid: false, error: max.message})
        }
        if (required && value === 0) {
          return this.setState({isValid: false, error: ''})
        }
        break
      default:
        if (min && value.length < min.value) {
          return this.setState({isValid: false, error: min.message})
        }
        if (max && value.length > max.value) {
          return this.setState({isValid: false, error: max.message})
        }
        if (required && !value.length) {
          return this.setState({isValid: false, error: ''})
        }
        break
    }

    this.setState({isValid: true, error: ''})
  }

  handleChange = ({target: {value}}) => {
    const {validators: {min, max, required}, type} = this.props

    if (type === 'duration') {
      const [minutes, seconds] = value.split(':')
      const currentTimeInSeconds = minutes * 60 + seconds
      if ((min && currentTimeInSeconds > min.value) ||
        (max && currentTimeInSeconds < max.value) ||
        (required && currentTimeInSeconds > 0)) {
        this.setState({isValid: true, error: ''})
      }
    } else {
      if ((min && value.length > min.value) ||
        (max && value.length < max.value) ||
        (required && !!value.length)) {
        this.setState({isValid: true, error: ''})
      }
    }
  }

  render() {
    let {classes, value, onChange, label, name, ...rest} = this.props
    return <FormControl>
      <InputLabel shrink htmlFor={name}>
        {label}
      </InputLabel>
      <Input
        value={value}
        onChange={e => {
          this.handleChange(e)
          onChange(e)
        }}
        name={name}
        onBlur={this.handleBlur}
        {...rest}
        error={!this.state.isValid}
      />
      <If condition={!this.state.isValid}>
        <Typography color='error'>
          {this.state.error}
        </Typography>
      </If>
    </FormControl>
  }
}

VTextField.defaultProps = {}

VTextField.propTypes = {
  validators: PropTypes.shape({
    min: PropTypes.shape({
      value: PropTypes.number.isRequired,
      message: PropTypes.string.isRequired
    }),
    max: PropTypes.shape({
      value: PropTypes.number.isRequired,
      message: PropTypes.string.isRequired
    }),
    required: PropTypes.bool
  })
}

export default VTextField