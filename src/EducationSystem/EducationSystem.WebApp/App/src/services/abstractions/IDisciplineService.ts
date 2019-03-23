import Theme from '../../models/Theme'
import Exception from '../../helpers/Exception'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import Discipline from '../../models/Discipline'

export default interface IDisciplineService {
   getAll(options?: IPagingOptions): Promise<IPagedData<Discipline> | Exception>
   getThemes(id: number): Promise<IPagedData<Theme> | Exception>
}