import Exception from '../../helpers/Exception'
import Theme from '../../models/Theme'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import Question from '../../models/Question'

export default interface IThemeService {
  add(theme: Theme): Promise<Theme | Exception>,
  delete(id: number): Promise<void | Exception>,
  update(theme: Theme): Promise<Theme | Exception>
  getQuestions(id: number, options?: IPagingOptions): Promise<IPagedData<Question> | Exception>
}