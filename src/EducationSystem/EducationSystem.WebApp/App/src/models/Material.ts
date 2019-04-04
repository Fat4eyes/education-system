import Model from './Model'
import DocumentFile from './DocumentFile'

export default class Material extends Model {
  public Name: string = ''
  public Template: string = ''
  public Files: Array<DocumentFile> = []
}