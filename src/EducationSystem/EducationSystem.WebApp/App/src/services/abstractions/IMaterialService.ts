import Exception from '../../helpers/Exception'
import Material from '../../models/Material'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import INameFilter from '../../models/Filters'

export default interface IMaterialService {
  add(material: Material): Promise<Material | Exception>
  update(material: Material): Promise<Material | Exception>
  get(id: number): Promise<Material | Exception>
  getAll(options?: IPagingOptions, filter?: INameFilter): Promise<IPagedData<Material> | Exception>
}