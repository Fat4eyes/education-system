export default class Mapper {
  private _rules: Map<string, Map<string, string>>
  
  constructor() {
    this._rules = new Map()
  }

  public rule(source: Function, rules: any): Mapper {
    this._rules.set(source.name, new Map(rules))
    
    return this
  }
  
  public map<D>(source: any, dest: new() => D): D {
    let result = new dest();
    
    for (const key in source) {
      if (key in result) {
        (<any>result)[key] = source[key]
      }
    }
    
    return result;
  }
}