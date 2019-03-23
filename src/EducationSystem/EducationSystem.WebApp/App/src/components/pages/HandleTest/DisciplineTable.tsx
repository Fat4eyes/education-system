import TableComponent from '../../Table/TableComponent'
import Discipline from '../../../models/Discipline'
import {ITableState} from '../../Table/IHandleTable'
import IPagedData, {IPagingOptions} from '../../../models/PagedData'
import {inject} from '../../../infrastructure/di/inject'
import IDisciplineService from '../../../services/abstractions/IDisciplineService'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import {Exception} from '../../../helpers'
import {Grid, TextField, Typography} from '@material-ui/core'
import * as React from 'react'
import {TablePagination} from '../../core'
import RowHeader from '../../Table/RowHeader'
import {ChangeEvent} from 'react'

interface IProps extends InjectedNotistackProps {
  handleClick: any
}

interface IState extends ITableState<Discipline> {
  Name: string
}

const minLengthForTrigger = 3

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
      IsLoading: false,
      Name: ''
    }
  }

  getTableData = async (param: IPagingOptions) => {
    let result = await this.DisciplineService!.getAll({
      Skip: 0,
      Take: this.state.CountPerPage,
      ...param
    },this.state.Name.length >= minLengthForTrigger ? {
      Name: this.state.Name
    } : {})

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
  
  handleName = ({target: {value}}: ChangeEvent<HTMLInputElement> | any) => {
    const oldValue = this.state.Name

    if (value.length >= minLengthForTrigger || (value.length < minLengthForTrigger && oldValue.length >= minLengthForTrigger)) {
      this.setState({
        Name: value,
        IsLoading: true,
        Page: 0
      }, () => this.getTableData({Skip: 0, Take: this.state.CountPerPage}))
    } else {
      this.setState({
        Name: value,
        IsLoading: true,
        Page: 0
      })
    }
  }

  componentDidMount() {
    this.setState({IsLoading: true}, () => this.getTableData({Skip: 0, Take: this.state.CountPerPage}))
  }

  render() {
    return <Grid item xs={12} container justify='center'>
      <TextField
        label='Название дисциплины'
        placeholder='Название дисциплины (больше 3 символов)'
        value={this.state.Name}
        onChange={this.handleName}
        style={{margin: '0 5px'}}
        fullWidth
        margin='none'
      />
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
        <RowHeader key={d.Id} onClick={() => this.props.handleClick(d)}>
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