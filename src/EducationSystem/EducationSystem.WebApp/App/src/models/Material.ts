import Model from './Model'
import DocumentFile from './DocumentFile'

export default class Material extends Model {
  public Name: string = ''
  public Template: string = ''
  public Files: Array<DocumentFile> = []
  public Anchors: Array<IMaterialAnchor> = []
}

export interface IMaterialAnchor {
  Id: number
  Name: string
  Token: string
}