export const fluent = (target, key, descriptor) => {
  const value = descriptor.value

  console.log(descriptor, value)

  descriptor.value = (...args) => {
    // console.log(this, args)
    // value.apply(this, args)

    return value.apply(this, args)
  }
  
  return descriptor
}