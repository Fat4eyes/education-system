import Exception from '../../helpers/Exception'
import Theme from '../../models/Theme'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import Question from '../../models/Question'

export default interface IFileService {
  add(form: FormData): Promise<any | Exception>,
  delete(id: number): Promise<any | Exception>
}