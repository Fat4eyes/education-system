import Model from './Model'
import {QuestionComplexityType, QuestionType} from '../common/enums'
import Answer from './Answer'
import Program from './Program'
import ImageFile from './ImageFile'
import Material, {IMaterialAnchor} from './Material'
import {IPagingOptions} from './PagedData'

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
  public Hash: string = ''
  public Save?: boolean
  public Right?: boolean
  public MaterialAnchors: Array<IMaterialAnchor> = []
  
  constructor(themeId?: number){
    super()
    
    this.ThemeId = themeId
  }
}

export interface IFilterQuestion extends IPagingOptions {
  ThemeId?: number
  TestId?: number
  Passed?: boolean
}

export interface QuestionOptions {
  WithAnswers?: boolean
  WithProgram?: boolean
  WithMaterial?: boolean
}