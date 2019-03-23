import Theme from '../../models/Theme'
import Exception from '../../helpers/Exception'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import Discipline from '../../models/Discipline'
import INameFilter from '../../models/Filters'

export default interface IDisciplineService {
   getAll(options?: IPagingOptions, filter?: INameFilter): Promise<IPagedData<Discipline> | Exception>
   getThemes(id: number): Promise<IPagedData<Theme> | Exception>
}