import React, {Component} from 'react'
import {Typography, withStyles} from '@material-ui/core'

const styles = () => ({
  root: {
    position: 'absolute',
    top: `50%`,
    left: `50%`,
    transform: `translate(-50%, -50%)`,
    textAlign: 'center'
  }
})

@withStyles(styles)
class Try extends Component {
  constructor(props) {
    super(props)
    this.state = {
      hasError: false,
      error: ''
    }
  }

  componentDidCatch(error) {
    this.setState({hasError: true, error: error.message})
  }

  render() {
    const {Catch, classes} = this.props
    if (this.state.hasError) {
      return Catch || <div className={classes.root}>
        <Typography variant="h5" component="h3">
          Упс, что-то пошло не так.
        </Typography>
        <Typography variant="h5" component="h3">
          Обратитесь к администратору.
        </Typography>
      </div>
    }
    return this.props.children
  }
}

export default Try
