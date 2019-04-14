import Model from './Model'
import Test from './Test'
import Theme from './Theme'
import {IPagingOptions} from './PagedData'
import INameFilter from './Filters'

export default class Discipline extends Model {
  public Name: string = ''
  public Abbreviation: string = ''
  public Description: string = ''
  public Tests: Array<Test> = []
  public Themes: Array<Theme> = []
}

export interface IOptionsDiscipline {
  WithTests?: boolean,
  WithThemes?: boolean
}

export interface IFilterDiscipline extends IPagingOptions, INameFilter {}
