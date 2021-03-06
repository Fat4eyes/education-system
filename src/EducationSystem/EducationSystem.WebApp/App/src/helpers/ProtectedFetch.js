import Cookies from 'js-cookie'
import Fetch from './Fetch'
import {authRoutes} from '../routes'
import {TokenCookieName} from '../constants'

class ProtectedFetch extends Fetch {
  static handleToken(onError) {
    let token = Cookies.get(TokenCookieName) || (onError && onError()) || false
    
    if (!token) return window.location = window.location.origin + '/signin' 
    
    return token
  }

  static async post(url, data, onError) {
    const token = ProtectedFetch.handleToken()
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

  static async put(url, data, onError) {
    const token = ProtectedFetch.handleToken()
    return token
      ? Fetch.handleFetch(url, {
        method: 'PUT',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: data
      }, onError)
      : null
  };

  static async postAndFiles(url, data, onError) {
    const token = ProtectedFetch.handleToken()
    return token
      ? Fetch.handleFetch(url, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`
        },
        body: data
      }, onError)
      : null
  };

  static async delete(url, data, onError) {
    const token = ProtectedFetch.handleToken()
    return token
      ? Fetch.handleFetch(url, {
        method: 'DELETE',
        headers: {
          'Authorization': `Bearer ${token}`
        },
        body: data
      }, onError)
      : null
  };

  static async get(url, onError) {
    const token = ProtectedFetch.handleToken()
    return token
      ? Fetch.handleFetch(url, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      }, onError)
      : null
  };

  static async check(onError) {
    const token = ProtectedFetch.handleToken()
    try {
      let response = await fetch(authRoutes.check, {
        headers: {'Authorization': `Bearer ${token}`},
        method: 'POST'
      })
      if (!response.ok) {
        throw response
      }
      return true
    } catch (e) {
      onError && onError(e)
      return false
    }
  };
}

export default ProtectedFetch

