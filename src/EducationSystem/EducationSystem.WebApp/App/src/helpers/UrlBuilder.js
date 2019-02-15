export default class UrlBuilder {
  static Build = (url, params) => {
    if (!params)
      return url

    url += '?'

    for (let key in params)
      url += `${key}=${params[key]}&`

    return url.slice(0, -1)
  }
}