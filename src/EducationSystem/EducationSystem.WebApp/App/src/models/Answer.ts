import Model from './Model'
import {AnswerStatus} from '../common/enums'

export default class Answer extends Model {
  public Text: string = ''
  public IsRight?: boolean
  public QuestionId?: number
  public Status?: AnswerStatus
  
  constructor(text: string, isRight?: boolean) {
    super()
    
    this.Text = text
    this.IsRight = isRight
  }
}