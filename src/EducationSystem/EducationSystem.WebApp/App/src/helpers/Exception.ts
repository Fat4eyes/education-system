type ExceptionType = 'error' | 'info' | 'warning' | null | undefined

export default class Exception {
  public message: string
  public type: ExceptionType
  
  constructor(message: string, type?: ExceptionType) {
    this.message = message
    this.type = type
  }
}