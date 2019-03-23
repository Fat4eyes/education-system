export interface ITableState<T> {
  Count: number,
  CountPerPage: number,
  Page: number,
  Items: Array<T>,
  IsLoading: boolean
}

export interface ITableFilterState<T> extends ITableState<T> {
  
}