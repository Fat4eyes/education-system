import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {If} from '../core'
import {Typography} from '@material-ui/core'
import TextField from '@material-ui/core/TextField'

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
        if (required && value !== 0) {
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

    switch (type) {
      case 'duration':
        const [minutes, seconds] = value.split(':')
        const currentTimeInSeconds = minutes * 60 + seconds

        if ((min && currentTimeInSeconds > min.value) ||
          (max && currentTimeInSeconds < max.value) ||
          (required && currentTimeInSeconds > 0)) {
          this.setState({isValid: true, error: ''})
        }
        break
      default:
        if ((min && value.length > min.value) ||
          (max && value.length < max.value) ||
          (required && !!value.length)) {
          this.setState({isValid: true, error: ''})
        }
        break
    }
  } 

  render() {
    let {classes, value, onChange, ...rest} = this.props
    return <>
      <TextField
        value={value}
        onChange={e => {
          this.handleChange(e)
          onChange(e)
        }}
        onBlur={this.handleBlur}
        {...rest}
        error={!this.state.isValid}
      />
      <If condition={!this.state.isValid}>
        <Typography color='error'>
          {this.state.error}
        </Typography>
      </If>
    </>
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