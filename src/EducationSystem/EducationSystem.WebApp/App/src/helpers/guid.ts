export class Guid {
  public static create = (): string => 
    'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
      .replace(/[xy]/g, (c: string) => {
        let r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
  })
}
