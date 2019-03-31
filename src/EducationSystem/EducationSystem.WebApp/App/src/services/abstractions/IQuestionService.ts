import Exception from '../../helpers/Exception'
import Question from '../../models/Question'

export default interface IQuestionService {
  add(question: Question): Promise<Question | Exception>,
}