import {Exception, ProtectedFetch} from '../../helpers'
import IFileService from '../abstractions/IFileService'
import {documentRoutes, imageRoutes} from '../../routes'
import {FileType} from '../../common/enums'
import FileModel from '../../models/FileModel'

export default class FileService implements IFileService {
  async add(form: FormData, type: FileType): Promise<FileModel | Exception> {
    switch (type) {
      case FileType.Image:
        return await ProtectedFetch.postAndFiles(imageRoutes.add(), form)
      case FileType.Document:
        return await ProtectedFetch.postAndFiles(documentRoutes.add(), form)
    }
    return new Exception(`${FileType[type]} не поддерживается`)
  }

  async delete(id: number, type: FileType): Promise<any | Exception> {
    switch (type) {
      case FileType.Image:
        return await ProtectedFetch.delete(imageRoutes.delete(id))
      case FileType.Document:
        return await ProtectedFetch.delete(documentRoutes.delete(id))
    }
    return new Exception(`${FileType[type]} не поддерживается`)
  }

  async getExtensions(type: FileType): Promise<Array<string> | Exception> {
    switch (type) {
      case FileType.Image:
        return await ProtectedFetch.get(imageRoutes.extensions())
      case FileType.Document:
        return await ProtectedFetch.get(documentRoutes.extensions())
    }
    return new Exception(`${FileType[type]} не поддерживается`)
  }
}