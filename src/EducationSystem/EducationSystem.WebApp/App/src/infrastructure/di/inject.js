import Container from './Container'

export const inject = (target, key, descriptor) => {
  descriptor.initializer = () => {
    return Container.getContainer().getService(key)
  }
  return descriptor
}

export const injectByName = name => (target, key, descriptor) => {
  descriptor.initializer = () => {
    return Container.getContainer().getService(name || key)
  }
  return descriptor
}