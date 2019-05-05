import {FileType} from '../common/enums'
import FileModel from '../models/FileModel'
import ProtectedFetch from '../helpers/ProtectedFetch'
import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'
import {getResult, IResult} from './IServices'

export default interface IFileService {
  add(form: FormData, type: FileType): Promise<IResult<FileModel>>,
  delete(id: number, type: FileType): Promise<any>,
  getExtensions(type: FileType): Promise<IResult<Array<string>>>
}

export class FileService implements IFileService {
  @inject private NotificationService?: INotificationService

  async add(form: FormData, type: FileType): Promise<IResult<FileModel>> {
    switch (type) {
      case FileType.Image:
        return getResult(
          await ProtectedFetch.postAndFiles(`/api/images`, form)
        )
      case FileType.Document:
        return getResult(
          await ProtectedFetch.postAndFiles(`/api/documents`)
        )
    }

    this.NotificationService!.showError(`${FileType[type]} не поддерживается`)
    return getResult()
  }

  async delete(id: number, type: FileType): Promise<any> {
    switch (type) {
      case FileType.Image:
        return getResult(
          await ProtectedFetch.delete(`/api/images/${id}`)
        )
      case FileType.Document:
        return getResult(
          await ProtectedFetch.delete(`/api/documents/${id}`)
        )
    }
    this.NotificationService!.showError(`${FileType[type]} не поддерживается`)
    return getResult()
  }

  async getExtensions(type: FileType): Promise<IResult<Array<string>>> {
    switch (type) {
      case FileType.Image:
        return getResult(
          await ProtectedFetch.get(`/api/images/extensions`)
        )
      case FileType.Document:
        return getResult(
          await ProtectedFetch.get(`/api/documents/extensions`)
        )
    }
    this.NotificationService!.showError(`${FileType[type]} не поддерживается`)
    return getResult()
  }
}