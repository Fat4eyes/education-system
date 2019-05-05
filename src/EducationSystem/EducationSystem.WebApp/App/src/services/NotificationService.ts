import {INotifierProps, NullNotifier} from '../providers/NotificationProvider'
import {Exception} from '../helpers'

export default interface INotificationService {
  showError(error: string | Exception): void
  showSuccess(message: string): void
  showInfo(message: string): void
}

export class NotificationService implements INotificationService {
  private _notifier: INotifierProps = NullNotifier

  constructor(...[notifier]: Array<any>) {
    this._notifier = notifier
  }

  public showError = (error: string | Exception) => {
    console.log(error)
    if (error instanceof Exception)
      this._notifier.error(error.message)
    else
      this._notifier.error(error)
  }

  public showSuccess(message: string) {
    this._notifier.success(message)
  }

  public showInfo(message: string) {
    this._notifier.info(message)
  }
}
