import Cookies from 'js-cookie'
import {TOKEN} from '../providers/AuthProvider/constants';
import Fetch from './Fetch';

class ProtectedFetch extends Fetch {
  static handleToken(onError) {
    return Cookies.get(TOKEN) || onError();
  }

  static async post(url, data, onError) {
    const token = ProtectedFetch.handleToken();
    return token
      ? Fetch.handleFetch(url, {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: data
      }, onError)
      : null
  };

  static async get(url, onError) {
    const token = ProtectedFetch.handleToken();
    return token
      ? Fetch.handleFetch(url, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      }, onError)
      : null
  };

  static async check(onError) {
    const token = ProtectedFetch.handleToken();
    try {
      let response = await fetch('/api/auth/check', {headers: {'Authorization': `Bearer ${token}`}});
      if (!response.ok) {
        throw response;
      }
    } catch (e) {
      onError(e)
    }
  };
}

export default ProtectedFetch

