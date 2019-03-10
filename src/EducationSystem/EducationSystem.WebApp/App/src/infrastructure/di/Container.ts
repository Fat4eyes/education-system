import {Exception} from '../../helpers'
import Service, {IService} from './Service'

export default class Container {
  private static _container?: Container
  
  private _services: Map<string, IService>
  private _isConfigured: boolean

  private constructor() {
    this._services = new Map()
    this._isConfigured = false

    Container._container = this
  }
  
  public static getContainer(): Container {
    return Container._container || new Container()
  }

  private _getName(service: Function, name?: string) {
    name = name || service.name

    if (!name) {
      throw new Exception('Неверно задано имя сервиса')
    }

    return name
  }

  private _register<T>(service: new() => T, name: string, isSingleton: boolean) {
    let _name = this._getName(service, name)

    if (!this._services.has(_name)) {
      this._services.set(_name, new Service(service, _name, isSingleton))
    }
    return this
  }

  public singleton<T>(service: new() => T, name: string) {
    return this._register(service, name, true)
  }

  public transient<T>(service: new() => T, name: string) {
    return this._register(service, name, false)
  }

  public getService(name: string) {
    if (!this._isConfigured) {
      throw new Exception(`Необходимо выполнить запуск контейнера`)
    }

    let service = this._services.get(name)

    if (!service) {
      throw new Exception(`Сервис с названием "${name}" не зарегистрирован`)
    }

    return service.instance()
  }

  public setUp(): void {
    this._services.forEach(value => value.setUp())
    this._isConfigured = true
  }
  
}