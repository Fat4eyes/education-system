import Model from './Model'
import {FileType} from '../common/enums'

export default class FileModel extends Model {
  public Path: string = ''
  public Guid?: string = ''
  public Name: string = ''
  public Type: FileType = FileType.Any
}