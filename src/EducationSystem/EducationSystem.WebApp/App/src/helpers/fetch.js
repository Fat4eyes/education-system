import Cookies from 'js-cookie'
import {TOKEN} from '../providers/AuthProvider/constants';

async function handleFetch(url, options, errorHandler) {
  try {
    let response = await fetch(url, options);
    console.log(response);
    if (!response.ok) {
      throw response;
    }
    return (await response.json());
  } catch (error) {
    const message = error.text ? await error.text() : error;
    if (errorHandler) {
      errorHandler(message);
    } else {
      console.log(message);
    }
  }
}

const protectedFetch = {
  post: async (url, data, onError, onUnAutorized) => {
    let token = Cookies.get(TOKEN);

    if (!token) {
      return onUnAutorized ? onUnAutorized() : -1;
    }

    return handleFetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify(data)
    })
  },
  get: async (url, onUnAutorized) => {
    let token = Cookies.get(TOKEN);
    console.log(token, 'get');

    if (!token) {
      console.log(token, 'onAutorized');
      return onUnAutorized ? onUnAutorized() : -1;
    }

    return handleFetch(url, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    }, onUnAutorized)
  },
  check: async (onUnAutorized) => {
    let token = Cookies.get(TOKEN);

    if (!token) {
      console.log(token, 'onAutorized');
      return onUnAutorized ? onUnAutorized() : -1;
    }

    try {
      let response = await fetch('/api/auth/check', {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
      if (!response.ok) {
        throw response;
      }
    } catch (e) {
      onUnAutorized(e)
    }
  }
};

const baseFetch = {
  post: (url, data, onError) => handleFetch(url, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: data
  }, onError),
  get: (url, onError) => handleFetch(url, undefined, onError)
};

export {
  protectedFetch,
  baseFetch
}

export default handleFetch
