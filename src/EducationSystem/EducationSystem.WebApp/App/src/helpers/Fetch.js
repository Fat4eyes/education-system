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

      if (error === undefined || error === null)
        return handleError('Упс, просто бэк не дали.');
      
      switch (typeof error) {
        case 'object':
          switch (error.status) {
            case 401:
              handleError('Вы не авторизованны');
              break;
            case 403:
              handleError('Не лезь, она тебя сожрет.(Недостаточно прав)');
              break;
            case 500:
              handleError(await error.text());
              break;
            default:
              console.log(error)
          }
          break;
        case 'string':
        default:
          handleError(error);
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
