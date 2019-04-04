import Exception from '../../helpers/Exception'
import Material from '../../models/Material'

export default interface IMaterialService {
  add(material: Material): Promise<Material | Exception>,
  update(material: Material): Promise<Material | Exception>
  get(id: number): Promise<Material | Exception>
}