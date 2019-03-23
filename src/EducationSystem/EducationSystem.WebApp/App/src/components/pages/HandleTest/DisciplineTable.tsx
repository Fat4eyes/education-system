import TableComponent from '../../Table/TableComponent'
import Discipline from '../../../models/Discipline'
import {ITableState} from '../../Table/IHandleTable'
import IPagedData, {IPagingOptions} from '../../../models/PagedData'
import {inject} from '../../../infrastructure/di/inject'
import IDisciplineService from '../../../services/abstractions/IDisciplineService'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import {Exception} from '../../../helpers'
import {Grid, Typography} from '@material-ui/core'
import * as React from 'react'
import {TablePagination} from '../../core'
import RowHeader from '../../Table/RowHeader'

interface IProps extends InjectedNotistackProps {
  handleClick: any
}

interface IState extends ITableState<Discipline> {

}

class DisciplineTable extends TableComponent<Discipline, IProps, IState> {
  @inject
  private DisciplineService?: IDisciplineService

  constructor(props: IProps) {
    super(props)

    this.state = {
      Count: 0,
      CountPerPage: 10,
      Page: 0,
      Items: [],
      IsLoading: false
    }
  }

  getTableData = async (param: IPagingOptions) => {
    let result = await this.DisciplineService!.getAll({
      Skip: 0,
      Take: this.state.CountPerPage,
      ...param
    })

    if (result instanceof Exception) {
      return this.props.enqueueSnackbar(result.message, {
        variant: 'error',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })
    }

    this.setState({
      Count: (result as IPagedData<Discipline>).Count,
      Items: (result as IPagedData<Discipline>).Items,
      IsLoading: false
    })
  }

  componentDidMount() {
    this.setState({IsLoading: true}, () => this.getTableData({Skip: 0, Take: this.state.CountPerPage}))
  }

  render() {
    return <Grid item xs={12} container>
      <TablePagination
        count={{
          all: this.state.Count,
          perPage: this.state.CountPerPage,
          current: this.state.Items.length
        }}
        page={this.state.Page}
        onPageChange={this.handleChangePage}
        onCountPerPageChange={this.handleChangeRowsPerPage}
      />
      {this.state.Items.map((d: Discipline) =>
        <RowHeader key={d.Id} onClick={() => this.props.handleClick(d.Id)}>
          <Grid item xs={12} container wrap='nowrap' zeroMinWidth>
            <Typography noWrap variant='subtitle1'>
              {d.Name}
            </Typography>
          </Grid>
        </RowHeader>
      )}
    </Grid>
  }
}

export default withSnackbar(DisciplineTable) as any