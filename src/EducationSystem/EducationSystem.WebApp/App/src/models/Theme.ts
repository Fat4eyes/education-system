import Model from './Model'

export default class Theme extends Model {
  public Name: string = ''
  public DisciplineId?: number
  public Questions?: Array<object>
}