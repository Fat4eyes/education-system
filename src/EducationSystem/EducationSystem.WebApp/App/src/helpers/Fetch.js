class Fetch {
  static async handleFetch(url, options, onError) {
    try {
      let response = await fetch(url, options);
      
      if (!response.ok) 
        throw response;
      
      const {success, data, error} = await response.json();
      
      if (success === true) 
        return data;

      throw error
      
    } catch (error) {
      const handleError = e => onError ? onError(e) : console.log(e);
      
      switch (typeof error) {
        case 'object':
          switch (error.status) {
            case 401:
              return handleError('Вы не авторизованны');
            case 403:
              return handleError('Не лезь, она тебя сожрет.(Недостаточно прав)');
            case 500:
              return handleError(await error.text());
            default:
              return console.log(error)
          }
        case 'string':
        default:
          if (error === undefined || error === null)
            return handleError('Упс, просто бэк не дали.');
          return handleError(error);
      }
    }
  }

  static async post(url, data, onError) {
    return Fetch.handleFetch(url, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: data
    }, onError)
  };

  static async get(url, onError) {
    return Fetch.handleFetch(url, undefined, onError)
  }
}

export default Fetch
