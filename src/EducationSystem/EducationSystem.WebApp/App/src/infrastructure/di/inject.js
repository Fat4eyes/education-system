import Container from './Container'

export const inject = (target, key, descriptor) => {
  descriptor.initializer = () => {
    return Container.getContainer().getService(key)
  }
  return descriptor
}