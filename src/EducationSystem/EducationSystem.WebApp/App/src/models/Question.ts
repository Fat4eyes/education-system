import Model from './Model'
import {QuestionComplexityType, QuestionType} from '../common/enums'
import Answer from './Answer'
import Program from './Program'
import ImageFile from './ImageFile'
import Material from './Material'

export default class Question extends Model {
  public Text: string = ''
  public Type: QuestionType = QuestionType.ClosedOneAnswer
  public Complexity: QuestionComplexityType = QuestionComplexityType.Low
  public Time: number = 0
  public ThemeId?: number
  public Answers: Array<Answer> = []
  public Program?: Program
  public Image?: ImageFile
  public Material?: Material
  public Order?: number
  
  constructor(themeId?: number){
    super()
    
    this.ThemeId = themeId
  }
}

export interface QuestionOptions {
  WithAnswers?: boolean
  WithProgram?: boolean
  WithMaterial?: boolean
}