import FileModel from './FileModel'
import {FileType} from '../common/enums'

export default class DocumentFile extends FileModel {
  constructor() {
    super()

    this.Type = FileType.Document
  }
}