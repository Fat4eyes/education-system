import Theme from '../../models/Theme'
import {Exception, ProtectedFetch, UrlBuilder} from '../../helpers'
import {questionRoutes} from '../../routes'
import {IPagingOptions} from '../../models/PagedData'
import IQuestionService from '../abstractions/IQuestionService'
import Question from '../../models/Question'

export default class QuestionService implements IQuestionService {
  async add(question: Question): Promise<Question | Exception> {
    return await ProtectedFetch.post(UrlBuilder.Build(questionRoutes.add), JSON.stringify(question))
  }
}