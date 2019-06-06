import Model from './Model'
import {TestType} from '../common/enums'
import Theme from './Theme'
import Discipline from './Discipline'
import TestData from './TestData'

export default class Test extends Model {
  public Subject: string = ''
  public IsActive: boolean = false
  public TotalTime: number = 0
  public Attempts: number = 1
  public Type: TestType = TestType.Control
  public DisciplineId?: number
  public Discipline?: Discipline
  public Themes: Array<Theme> = []
  public ThemesCount: number = 0
  public QuestionsCount: number = 0
  public PassedThemesCount?: number = 0
  public PassedQuestionsCount?: number = 0
  
  constructor(disciplineId?: number) {
    super()
    
    this.DisciplineId = Number(disciplineId)
  }
}