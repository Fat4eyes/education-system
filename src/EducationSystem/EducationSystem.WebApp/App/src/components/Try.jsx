import React, {Component} from 'react'

class Try extends Component {
  constructor(props) {
    super(props);
    this.state = {
      hasError: false
    };
  }

  componentDidCatch(error, info) {
    this.setState({hasError: true});
  }

  render() {
    const {Catch} = this.props;
    if (this.state.hasError) {
      return Catch || <h1>:c</h1>;
    }
    return this.props.children;
  }
}

export default Try
