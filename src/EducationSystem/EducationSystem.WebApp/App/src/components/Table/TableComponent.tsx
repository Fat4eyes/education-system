import * as React from 'react'
import {Component} from 'react'
import {IPagingOptions} from '../../models/PagedData'
import {ITableState} from './IHandleTable'
import INameFilter from '../../models/Filters'

export default abstract class TableComponent<TItem, TProps, TState extends ITableState<TItem>> extends Component<TProps, TState> {
  protected minLengthForTrigger: number = 3
  
  abstract async getTableData(param: IPagingOptions): Promise<any>
  
  getNameFilter = (value: string): INameFilter => (value.length >= this.minLengthForTrigger ? {Name: value} : {})
  isNeedToReloadData = (newValue: string, oldValue: string) =>
    newValue.length >= this.minLengthForTrigger || 
    (newValue.length < this.minLengthForTrigger && oldValue.length >= this.minLengthForTrigger)
  
  handleChangePage = async (page: number) =>
    this.setState({Page: page, IsLoading: true},
      async () =>
        await this.getTableData({
          Skip: page * this.state.CountPerPage,
          Take: this.state.CountPerPage,
        }))

  handleChangeRowsPerPage = (value: number) => {
    if (value === this.state.CountPerPage) return
    this.setState({
      CountPerPage: value,
      Page: 0,
      IsLoading: true
    }, async () => {
      await this.getTableData({
        Skip: 0,
        Take: value
      })
    })
  }
}