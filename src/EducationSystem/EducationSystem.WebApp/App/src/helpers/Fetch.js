import history from '../history'

class Fetch {
  static async handleFetch(url, options, onError) {
    try {
      let response = await fetch(url, options);
      
      if (!response.ok) 
        throw response;
      
      const {Success, Data, Error} = await response.json();
      
      if (Success === true) 
        return Data;

      throw Error
      
    } catch (Error) {
      const handleError = e => {
        e = e.trim();
        if (e[e.length - 1] === '.') {
          e = e.slice(0, -1)
        } 
        onError ? onError(e) : console.log(e)
      };
      
      switch (typeof Error) {
        case 'object':
          switch (Error.status) {
            case 401:
              history.push('/signin');
              return handleError('Вы не авторизованны');
            case 403:
              return handleError('Не лезь, она тебя сожрет.(Недостаточно прав)');
            case 500:
              return handleError(await Error.text());
            default:
              return console.log(Error)
          }
        case 'string':
        default:
          if (Error === undefined || Error === null)
            return handleError('Упс, просто бэк не дали.');
          return handleError(Error);
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
