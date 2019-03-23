import Model from './Model'
import Test from './Test'
import Theme from './Theme'

export default class Discipline extends Model {
  public Name: string = ''
  public Abbreviation: string = ''
  public Description: string = ''
  public Tests: Array<Test> = []
  public Themes: Array<Theme> = []
}