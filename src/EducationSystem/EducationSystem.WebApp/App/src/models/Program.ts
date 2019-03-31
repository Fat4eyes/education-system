import Model from './Model'
import {LanguageType} from '../common/enums'
import ProgramData from './ProgramData'
import Question from './Question'

export default class Program extends Model {
  public QuestionId?: number
  public Question?: Question
  public Template: string = ''
  public LanguageType?: LanguageType = LanguageType.CPP
  public TimeLimit: number = 0
  public MemoryLimit: number = 0
  public ProgramDatas: Array<ProgramData> = []
}