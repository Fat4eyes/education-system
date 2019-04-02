import {Exception, ProtectedFetch} from '../../helpers'
import IFileService from '../abstractions/IFileService'
import ImageFile from '../../models/ImageFile'
import {imageRoutes} from '../../routes'

export default class FileService implements IFileService {
  async addImage(form: FormData): Promise<ImageFile | Exception> {
    return await ProtectedFetch.postAndFiles(imageRoutes.add(), form)
  }

  async deleteImage(id: number): Promise<any | Exception> {
    return await ProtectedFetch.delete(imageRoutes.delete(id))
  }
}