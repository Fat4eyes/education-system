export interface IService {
  instance: () => object
  setUp: () => void
}

export default class Service<T> implements IService {
  private _name: string
  private readonly _isSingleton: boolean
  private readonly _class: new() => T
  private _object?: any

  constructor(service: new() => T, name: string, isSingleton: boolean) {
    this._name = name
    this._isSingleton = isSingleton
    this._class = service
  }

  public instance(): any {
    if (this._isSingleton) {
      return this._object as object
    }

    return new this._class()
  }

  public setUp(): void {
    if (this._isSingleton) {
      this._object = new this._class()
    }
  }
}