import TableComponent from './TableComponent'
import Material from '../../models/Material'
import {ITableState} from './IHandleTable'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import IMaterialService from '../../services/abstractions/IMaterialService'
import {inject} from '../../infrastructure/di/inject'
import {INotifierProps, withNotifier} from '../../providers/NotificationProvider'
import * as React from 'react'

interface IProps {

}

type TProps = INotifierProps & IProps

interface IState extends ITableState<Material> {
  Name: string
}

class MaterialSelect extends TableComponent<Material, TProps, IState> {
  @inject private readonly MaterialService?: IMaterialService

  constructor(props: TProps) {
    super(props)

    this.state = {
      Count: 0,
      CountPerPage: 10,
      Page: 0,
      Items: [],
      IsLoading: false,
      Name: ''
    }
  }

  async getTableData(param: IPagingOptions): Promise<any> {
    let result = await this.MaterialService!.getAll({
      Skip: 0,
      Take: this.state.CountPerPage,
      ...param
    }, this.getNameFilter(this.state.Name))

    // if (result instanceof Exception) {
    //   return this.props.error(result.message)
    // }

    this.setState({
      Count: (result as IPagedData<Material>).Count,
      Items: (result as IPagedData<Material>).Items,
      IsLoading: false
    })
  }

  render(): React.ReactNode {
    console.log(this.props)
    return <div/>
  }

}

export default withNotifier(MaterialSelect) as any