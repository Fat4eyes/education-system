import Model from './Model'
import {CodeRunStatus, LanguageType} from '../common/enums'
import ProgramData from './ProgramData'
import Question from './Question'

export default class Program extends Model {
  public QuestionId?: number
  public Question?: Question
  public Template: string = ''
  public Source: string = ''
  public LanguageType?: LanguageType = LanguageType.CPP
  public TimeLimit: number = 0
  public MemoryLimit: number = 0
  public ProgramDatas: Array<ProgramData> = []
  public CodeExecutionResult?: ICodeExecutionResult
}

export interface ICodeExecutionResult {
  Success: boolean
  Errors: Array<string>
  Results: Array<ICodeRunResult>
}

export interface ICodeRunResult {
  UserOutput: string
  ExpectedOutput: string
  Status: CodeRunStatus
  Success: boolean
}