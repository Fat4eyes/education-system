import {Exception, ProtectedFetch, UrlBuilder} from '../../helpers'
import {questionRoutes} from '../../routes'
import IQuestionService from '../abstractions/IQuestionService'
import Question, {QuestionOptions} from '../../models/Question'

export default class QuestionService implements IQuestionService {
  async add(question: Question): Promise<Question | Exception> {
    return await ProtectedFetch.post(UrlBuilder.Build(questionRoutes.add), JSON.stringify(question))
  }

  async update(question: Question): Promise<Question | Exception> {
    return await ProtectedFetch.put(UrlBuilder.Build(questionRoutes.update(question.Id)), JSON.stringify(question))
  }

  async get(id: number, questionOptions?: QuestionOptions): Promise<Question | Exception> {
    return await ProtectedFetch.get(UrlBuilder.Build(questionRoutes.get(id), questionOptions))
  }
}