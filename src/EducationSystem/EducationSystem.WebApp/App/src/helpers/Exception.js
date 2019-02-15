export default class Exception {
  constructor(message, type = null) {
    this.message = message
    this.type = type
  }
}