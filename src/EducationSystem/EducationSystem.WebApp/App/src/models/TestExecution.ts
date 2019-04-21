import Model from './Model'
import {TestType} from '../common/enums'
import Theme from './Theme'
import Discipline from './Discipline'
import TestData from './TestData'
import Answer from './Answer'
import Question from './Question'

export default class TestExecution extends Model {
  public Questions: Array<Question> = []
  public TestData?: TestData
}