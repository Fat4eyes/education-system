import Exception from '../../helpers/Exception'
import {FileType} from '../../common/enums'
import FileModel from '../../models/FileModel'

export default interface IFileService {
  add(form: FormData, type: FileType): Promise<FileModel | Exception>,
  delete(id: number, type: FileType): Promise<any | Exception>,
  getExtensions(type: FileType): Promise<Array<string> | Exception>
}