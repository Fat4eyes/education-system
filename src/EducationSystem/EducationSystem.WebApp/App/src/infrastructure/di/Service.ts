export interface IService {
  instance: () => object
  setUp: () => void
}

export default class Service<T> implements IService {
  private _name: string
  private readonly _isSingleton: boolean
  private readonly _class: new(...params: Array<any>) => T
  private _object?: any
  private _already: boolean
  private _params: Array<any> | undefined

  constructor(service: new(...params: Array<any>) => T, name: string, isSingleton: boolean, params?: Array<any>) {
    this._name = name
    this._isSingleton = isSingleton
    this._class = service
    this._already = false
    this._params = params
  }

  public instance(): any {
    if (this._isSingleton) {
      return this._object as object
    }

    if (this._params) {
      return new this._class(...this._params)
    } else {
      return new this._class()
    }
  }

  public setUp(): void {
    if (this._already) return
    
    if (this._isSingleton) {
      if (this._params) {
        this._object = new this._class(...this._params)
      } else {
        this._object = new this._class()
      }
    }
    this._already = true
  }
}