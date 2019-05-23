import * as React from 'react'
import {Component, FC} from 'react'
import {TNotifierProps, withNotifier} from '../../providers/NotificationProvider'
import {TAuthProps} from '../../providers/AuthProvider/AuthProviderTypes'
import {TUserRole} from '../../common/enums'
import {Redirect, Route, RouteProps} from 'react-router'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider'
import {Guid} from '../../helpers/guid'

interface IProps extends RouteProps {
  roles?: Array<TUserRole>
  title: string
}

type TProps = IProps & TAuthProps & TNotifierProps

interface IPageProps {
  title: string
  component: any

  [prop: string]: any
}

class Page extends Component<IPageProps> {
  componentDidMount(): void {
    document.title = `Система обучения – ${this.props.title}`
  }

  render() {
    const {component: Component, ...rest} = this.props
    return <Component {...rest}/>
  }
}

class RouteProxy extends Component<TProps> {
  componentDidMount(): void {
    !this.props.auth.checkAuth(this.props.roles) && this.props.notifier.error('Недостаточно прав')
  }

  render() {
    const {roles, auth, notifier, component, title, ...rest} = this.props

    return <Route
      {...rest}
      render={
        props => {
          if (auth.checkAuth(roles)) {
            return <Page component={component} title={title} {...props}/>
          }

          return <Redirect to={{pathname: '/signin', state: {from: props.location}}}/>
        }
      }
    />
  }
}

export default withAuthenticated(withNotifier(RouteProxy)) as FC<IProps>