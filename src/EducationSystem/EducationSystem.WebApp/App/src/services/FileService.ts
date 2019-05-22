import {FileType} from '../common/enums'
import FileModel from '../models/FileModel'
import ProtectedFetch from '../helpers/ProtectedFetch'
import {inject} from '../infrastructure/di/inject'
import INotificationService from './NotificationService'
import {getResult, IResult} from './IServices'
import IPagedData, {IPagingOptions} from '../models/PagedData'
import UrlBuilder from '../helpers/UrlBuilder'

export default interface IFileService {
  add(form: FormData, type: FileType): Promise<IResult<FileModel>>
  delete(id: number, type: FileType): Promise<any>
  getExtensions(type: FileType): Promise<IResult<Array<string>>>
  getAll(type: FileType, options?: IPagingOptions): Promise<IResult<IPagedData<FileModel>>>
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
          await ProtectedFetch.postAndFiles(`/api/documents`, form)
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

  async getAll(type: FileType, options?: IPagingOptions): Promise<IResult<IPagedData<FileModel>>> {
    switch (type) {
      case FileType.Image:
        return getResult(
          await ProtectedFetch.get(UrlBuilder.Build(`/api/images`, {...options, Type: type}))
        )
      case FileType.Document:
        return getResult(
          await ProtectedFetch.get(UrlBuilder.Build(`/api/documents`, {...options, Type: type}))
        )
    }
    this.NotificationService!.showError(`${FileType[type]} не поддерживается`)
    return getResult()
  }

}