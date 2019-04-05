import * as React from 'react'
import {OptionsObject, VariantType} from 'notistack'
import {SnackbarOrigin} from '@material-ui/core/Snackbar'

type THandler = (message: string | React.ReactNode, options?: OptionsObject) => string | number

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

export interface IEventDispatcher {
  send(event: IEvent): void
}

export default class EventDispatcher implements IEventDispatcher {
  private readonly _handler: THandler
  private _anchorParams: SnackbarOrigin

  constructor(handler: THandler) {
    this._handler = handler
    this._anchorParams = {
      vertical: 'bottom',
      horizontal: 'right'
    } as SnackbarOrigin
  }

  public setAnchorParams(params: SnackbarOrigin) {
    this._anchorParams = params
  }

  public send(event: IEvent): void {
    this._handler(event.message, {
      variant: EventType[event.type] as VariantType,
      anchorOrigin: this._anchorParams
    })
  }
}