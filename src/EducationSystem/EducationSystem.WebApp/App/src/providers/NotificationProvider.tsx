import * as React from 'react'
import {Component, createContext} from 'react'
import {InjectedNotistackProps, VariantType, withSnackbar} from 'notistack'

export enum EventType {
  default,
  error,
  success,
  warning,
  info
}

export interface IEvent {
  type: EventType,
  message: string
}

interface INotifierProps {
  notify: (event: IEvent) => void
  error: (message: string) => void
  success: (message: string) => void
  info: (message: string) => void
}

export type TNotifierProps = {
  notifier: INotifierProps
}

const NullNotifier: INotifierProps = {
  notify: (event: IEvent): void => console.log(event.message, EventType[event.type]),
  error: function (message: string) {this.notify({message, type: EventType.error})},
  success: function (message: string) {this.notify({message, type: EventType.success})},
  info: function (message: string) {this.notify({message, type: EventType.info})}
}

const {Provider, Consumer} = createContext(NullNotifier)

class NotificationProvider extends Component<InjectedNotistackProps & {notifier?: INotifierProps}> {
  private _baseNotifier: INotifierProps = {
    notify: (event: IEvent): void => {
      this.props.enqueueSnackbar(event.message, {
        variant: EventType[event.type] as VariantType,
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })
    },
    error: (message: string) => this._baseNotifier.notify({message, type: EventType.error}),
    success: (message: string) => this._baseNotifier.notify({message, type: EventType.success}),
    info: (message: string) => this._baseNotifier.notify({message, type: EventType.info})
  }

  render(): React.ReactNode {
    return <Provider value={this.props.notifier || this._baseNotifier}>
      {this.props.children}
    </Provider>
  }
}

export const withNotifier = (Component: any) => (props: any) =>
  <Consumer>
    {notifier => <Component {...props} notifier={notifier}/>}
  </Consumer>

export default withSnackbar(NotificationProvider) as any