import * as React from 'react'
import {Component} from 'react'
import {IPagingOptions} from '../../models/PagedData'
import {ITableState} from './IHandleTable'
import INameFilter from '../../models/Filters'

export default abstract class TableComponent<TItem, TProps, TState extends ITableState<TItem>> extends Component<TProps, TState> {
  set isMounted(value: boolean) {
    this._isMounted = value
  }
  
  protected minLengthForTrigger: number = 3
  private _isMounted: boolean = false

  abstract async getTableData(param?: IPagingOptions): Promise<any>

  protected constructor(props: TProps) {
    super(props)
    this._isMounted = true
  }

  componentWillUnmount(): void {
    this._isMounted = false
  }

  public setState = <K extends keyof TState>(
    state: ((prevState: Readonly<TState>, props: Readonly<TProps>) => 
      (Pick<TState, K> | TState | null)) | (Pick<TState, K> | TState | null),
    callback?: () => void) => this._isMounted && super.setState(state, callback)

  getNameFilter = (value: string): INameFilter => (value.length >= this.minLengthForTrigger ? {Name: value} : {})
  isNeedToReloadData = (newValue: string, oldValue: string) =>
    newValue.length >= this.minLengthForTrigger ||
    (newValue.length < this.minLengthForTrigger && oldValue.length >= this.minLengthForTrigger)

  handleChangePage = async (page: number) =>
    this.setState({Page: page, IsLoading: true},
      async () =>
        await this.getTableData({
          Skip: page * this.state.CountPerPage,
          Take: this.state.CountPerPage
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