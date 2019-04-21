import IThemeService from '../abstractions/IThemeService'
import Theme from '../../models/Theme'
import {Exception, ProtectedFetch, UrlBuilder} from '../../helpers'
import {themeRoutes} from '../../routes'
import {IPagingOptions} from '../../models/PagedData'
import Question from '../../models/Question'

export default class ThemeService implements IThemeService {
  async add(theme: Theme): Promise<Theme | Exception> {
    return await ProtectedFetch.post(UrlBuilder.Build(themeRoutes.add), JSON.stringify(theme))
  }

  async getQuestions(id: number, options?: IPagingOptions) {
    return await ProtectedFetch.get(UrlBuilder.Build(themeRoutes.getQuestions(id), options))
  }

  async delete(id: number): Promise<void | Exception> {
    return await ProtectedFetch.delete(UrlBuilder.Build(themeRoutes.delete(id)))
  }

  async update(theme: Theme): Promise<Theme | Exception> {
    return await ProtectedFetch.put(UrlBuilder.Build(themeRoutes.update(theme.Id!)), JSON.stringify(theme))
  }

  async updateThemeQuestions(id: number, questions: Array<Question>): Promise<void | Exception> {
    return await ProtectedFetch.put(UrlBuilder.Build(themeRoutes.updateThemeQuestions(id)), JSON.stringify(questions))
  }
}