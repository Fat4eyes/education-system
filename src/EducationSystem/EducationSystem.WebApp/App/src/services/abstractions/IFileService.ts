import Exception from '../../helpers/Exception'
import ImageFile from '../../models/ImageFile'

export default interface IFileService {
  addImage(form: FormData): Promise<ImageFile | Exception>,
  deleteImage(id: number): Promise<any | Exception>
}