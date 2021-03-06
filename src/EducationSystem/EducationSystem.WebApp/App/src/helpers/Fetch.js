import Exception from './Exception'

class Fetch {
  static _fetch = null

  constructor(spinnerProvider, errorHandler) {
    Fetch._fetch = this
    this._spinnerProvider = spinnerProvider
    this._errorHandler = errorHandler
  }
  
  static instance(spinnerProvider, errorHandler) {
    return Fetch._fetch || new Fetch(spinnerProvider, errorHandler)
  }

  static async handleFetch(url, options, onError) {
    const {_spinnerProvider, _errorHandler} = Fetch.instance()
    
    try {
      if (_spinnerProvider) _spinnerProvider.enable()

      let response = await fetch(url, options)

      if (!response.ok)
        throw response

      const {Success, Data, Error} = await response.json()

      if (_spinnerProvider) _spinnerProvider.disable()

      if (Success === true)
        return Data || true

      throw Error

    } catch (Error) {
      const handleError = e => {
        e = e.trim()
        if (e[e.length - 1] === '.') {
          e = e.slice(0, -1)
        }
        
        if (onError) {
          onError(e)
          return null
        } else {
          if (typeof _errorHandler === 'function') {
            _errorHandler(e)
            return null
          }
          return new Exception(e)
        }
      }

      if (_spinnerProvider) _spinnerProvider.disable()

      switch (typeof Error) {
        case 'object':
          switch (Error.status) {
            case 401:
              window.location = window.location.origin + '/signin'
              return handleError('Вы не авторизованны')
            case 403:
              return handleError('Не лезь, она тебя сожрет.(Недостаточно прав)')
            case 500:
              return handleError(await Error.text())
            default:
              return handleError('')
          }
        case 'string':
        default:
          if (Error === undefined || Error === null)
            return handleError('Упс, просто бэк не дали.')
          return handleError(Error)
      }
    }
  }

  static async post(url, Data, onError) {
    return Fetch.handleFetch(url, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(Data)
    }, onError)
  };

  static async get(url, onError) {
    return Fetch.handleFetch(url, undefined, onError)
  }
}

export default Fetch
