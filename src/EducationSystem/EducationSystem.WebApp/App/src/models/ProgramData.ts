import Model from './Model'
import Program from './Program'

export default class ProgramData extends Model {
  public ProgramId?: number
  public Program?: Program
  public Input: string = ''
  public ExpectedOutput: string = ''
  
  constructor(input: string, expectedOutput: string) {
    super()
    
    this.Input = input
    this.ExpectedOutput = expectedOutput
  }
}