import Exception from '../../helpers/Exception'
import Question, {QuestionOptions} from '../../models/Question'

export default interface IQuestionService {
  add(question: Question): Promise<Question | Exception>,
  update(question: Question): Promise<Question | Exception>
  get(id: number, questionOptions?: QuestionOptions): Promise<Question | Exception>
}