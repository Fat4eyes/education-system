import Model from './Model'
import {QuestionComplexityType, QuestionType} from '../common/enums'
import Answer from './Answer'

export default class Question extends Model {
  public Text: string = ''
  public Type?: QuestionType
  public Complexity?: QuestionComplexityType
  public Time: number = 0
  public ThemeId?: number
  public Answers: Array<Answer> = []
}