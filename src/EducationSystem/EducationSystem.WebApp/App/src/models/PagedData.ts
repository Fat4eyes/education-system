export default interface IPagedData<T> {
  Items: Array<T>
  Count: number
}

export interface IPagingOptions {
  Skip: number,
  Take: number,
  All?: boolean
}