import {Exception, ProtectedFetch} from '../../helpers'
import IFileService from '../abstractions/IFileService'

export default class FileService implements IFileService {
  async add(form: FormData): Promise<any | Exception> {
    return await ProtectedFetch.postAndFiles('/api/images', form)
  }

  async delete(id: number): Promise<any | Exception> {
    return await ProtectedFetch.delete('/api/images/' + id)
  }
}