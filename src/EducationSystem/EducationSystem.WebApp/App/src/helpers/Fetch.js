class Fetch {
  static async handleFetch(url, options, onError) {
    try {
      let response = await fetch(url, options);
      console.log(response);
      if (!response.ok) {
        throw response;
      }
      return (await response.json());
    } catch (error) {
      const message = error.text ? await error.text() : error;
      if (onError) {
        onError(message);
      } else {
        console.log(message);
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
