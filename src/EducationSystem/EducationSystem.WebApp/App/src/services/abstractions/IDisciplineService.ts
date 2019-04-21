import Theme from '../../models/Theme'
import Exception from '../../helpers/Exception'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import Discipline from '../../models/Discipline'
import INameFilter from '../../models/Filters'

export default interface IDisciplineService {
   getAll(options?: IPagingOptions, filter?: INameFilter): Promise<IPagedData<Discipline> | Exception>
   getThemes(id: number, options?: IPagingOptions): Promise<IPagedData<Theme> | Exception>
   updateDisciplineThemes(id: number, themes: Array<Theme>): Promise<void | Exception>
}