export interface ITableState<T> {
  Count: number,
  CountPerPage: number,
  Page: number,
  Items: Array<T>,
  IsLoading: boolean
}

export const getDefaultTableState = <T>(): ITableState<T> => ({
  Count: 0,
  CountPerPage: 10,
  Page: 0,
  Items: [],
  IsLoading: false
})


export interface ITableFilterState<T> extends ITableState<T> {
  
}