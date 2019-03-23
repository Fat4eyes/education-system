import IThemeService from '../abstractions/IThemeService'
import Theme from '../../models/Theme'
import {Exception, ProtectedFetch, UrlBuilder} from '../../helpers'
import {themeRoutes} from '../../routes'

export default class ThemeService implements IThemeService {
  async add(theme: Theme): Promise<Theme | Exception> {
    return await ProtectedFetch.post(UrlBuilder.Build(themeRoutes.add), JSON.stringify(theme));
  }
}