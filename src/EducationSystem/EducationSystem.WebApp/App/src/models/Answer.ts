import Model from './Model'

export default class Answer extends Model {
  public Text: string = ''
  public IsRight?: boolean
  public QuestionId?: number
  
  constructor(text: string, isRight?: boolean) {
    super()
    
    this.Text = text
    this.IsRight = isRight
  }
}