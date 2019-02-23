import {Exception} from './helpers'

class Service {
  constructor({service, name, isSingleton = false}) {
    const serviceType = typeof service

    if (serviceType !== 'function') {
      throw new Exception(`Сервис имеет некорректный тип: ${serviceType}`)
    }

    this.model = {
      Name: name,
      IsSingleton: isSingleton,
      It: service
    }
  }

  instance = () =>
    this.model.IsSingleton
      ? this.model.It
      : new this.model.It()
  
  setUp = () => {
    if (this.model.IsSingleton) {
      this.model.It = new this.model.It()
    }
  }
}

export default class Container {
  constructor() {
    this._services = new Map()
    this.isConfigured = false
  }

  _getName = (service, name) => {
    name = name || service.name

    if (!name) {
      throw new Exception('Неверно задано имя сервиса')
    }
    
    return name
  }

  _register = (service, name, isSingleton) => {
    let _name = this._getName(service, name)

    if (!this._services.has(_name)) {
      this._services.set(_name, new Service({
        service, name: _name, isSingleton: isSingleton
      }))
    }
    return this
  }

  singleton = (service, name) => this._register(service, name, true)
  transient = (service, name) => this._register(service, name, false)

  getService = name => {
    if (!this.isConfigured) {
      throw new Exception(`Необходимо выполнить запуск контейнера`)
    }
    
    let service = this._services.get(name)

    if (!service) {
      throw new Exception(`Сервис с названием "${name}" не зарегистрирован`)
    }

    return service.instance()
  }

  setUp = () => { 
    this._services.forEach(value => value.setUp())
    this.isConfigured = true
  }
}

let container = new Container()

const inject = (target, key, descriptor) => {
  descriptor.initializer = () => container.getService(key)
  return descriptor
}

export {
  container,
  inject
}


