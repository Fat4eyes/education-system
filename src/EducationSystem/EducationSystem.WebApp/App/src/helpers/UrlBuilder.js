export default class UrlBuilder {
  static Build = (url, params) => {
    if (!params)
      return url

    url = url + '?'

    for (let key in params)
      url += `${key}=${params[key]}&`

    if (url[url.length - 1] === '&')
      url = url.slice(0, -1)

    return url
  }
}