import {Component} from 'react'
import PropTypes from 'prop-types'
import {Fetch as NotProtectedFetch, ProtectedFetch, Snackbar} from '../../helpers'
import {withSnackbar} from 'notistack'

@withSnackbar
class Fetch extends Component {
  constructor(props) {
    super(props)

    this.state = {
      data: {},
      error: null,
      isLoading: false
    }

    this.Snackbar = new Snackbar(this.props.enqueueSnackbar)
  }

  componentDidMount() {
    this.setState({isLoading: true}, async () => {
      const {url, type, params, withToken} = this.props
      const {post, get} = withToken ? ProtectedFetch : NotProtectedFetch

      try {
        const data = await (type === 'POST' ? post(url, JSON.stringify(params)) : get(url))
        this.setState({data, isLoading: false})
      } catch (error) {
        this.Snackbar.Error(error)
        this.setState({error, isLoading: false})
      }
    })
  }

  render() {
    return this.props.children(this.state)
  }
}

Fetch.propTypes = {
  children: PropTypes.func.isRequired,
  url: PropTypes.string.isRequired,
  type: PropTypes.oneOf(['GET', 'POST']),
  params: PropTypes.object,
  withToken: PropTypes.bool
}

Fetch.defaultProps = {
  type: 'GET',
  params: {},
  withToken: false
}

export default Fetch