type ExceptionType = 'error' | 'info' | 'warning' | null | undefined

export default class Exception {
  public message: string
  public type: ExceptionType

  constructor(message: string, type?: ExceptionType) {
    this.message = message
    this.type = type
  }
}

export const handleResult = <T>(
  result: T | Exception,
  onFail: (message: string) => void, onSuccess: (data: T) => any
): void | T => {
  if (result instanceof Exception) {
    onFail(result.message)

    return onFail(result.message)
  }

  if (result as T) {
    onSuccess && onSuccess(result as T)

    return result
  }
}
export const resultHandler = {
  onError: (onError: ((message: string) => void | undefined) = message => console.log(message)) => ({
    onSuccess: <T>(onSuccess: (data: T) => any) => ({
      handleResult: (result: T | Exception) => {
        (result instanceof Exception) ? onError(result.message) : onSuccess(result as T)
      }
    })
  })
}
export const voidHandler = {
  onError: (onError: ((message: string) => void | undefined) = message => console.log(message)) => ({
    onSuccess: <T>(onSuccess?: () => any) => ({
      handleResult: (result: void | Exception) => {
        (result instanceof Exception) ? onError(result.message) : onSuccess && onSuccess()
      }
    })
  })
}