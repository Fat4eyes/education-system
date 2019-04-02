import FileModel from './FileModel'
import {FileType} from '../common/enums'

export default class ImageFile extends FileModel {
  constructor() {
    super()
    
    this.Type = FileType.Image
  }
}