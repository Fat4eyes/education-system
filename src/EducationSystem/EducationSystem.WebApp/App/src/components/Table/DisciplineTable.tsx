import {InjectedNotistackProps, withSnackbar} from 'notistack'
import {ITableState} from './IHandleTable'
import {Grid, TextField, Typography} from '@material-ui/core'
import {IPagingOptions} from '../../models/PagedData'
import RowHeader from './RowHeader'
import TableComponent from './TableComponent'
import IDisciplineService from '../../services/DisciplineService'
import * as React from 'react'
import {ChangeEvent} from 'react'
import Discipline from '../../models/Discipline'
import {inject} from '../../infrastructure/di/inject'
import {TablePagination} from '../core'
import BlockContent from '../Blocks/BlockContent'

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
    const {data, success} = await this.DisciplineService!.getAll({
      Skip: 0,
      Take: this.state.CountPerPage,
      ...param
    }, this.getNameFilter(this.state.Name))

    if (success && data) this.setState({Count: data.Count, Items: data.Items})
  }

  private handleName = ({target: {value}}: ChangeEvent<HTMLInputElement> | any) => {
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
    this.getTableData({Skip: 0, Take: this.state.CountPerPage})
  }

  render() {
    return <Grid item xs={12} container justify='center'>
      <Grid item xs={12}>
        <BlockContent bottom>
          <TextField
            autoFocus
            label='Название дисциплины'
            placeholder='Название дисциплины (больше 3 символов)'
            value={this.state.Name}
            onChange={this.handleName}
            fullWidth
            margin='none'
          />
        </BlockContent>
      </Grid>
      <Grid item xs={12}>
        <BlockContent bottom>
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
        </BlockContent>
      </Grid>
      <Grid item xs={12}>
        <BlockContent bottom>
          {this.state.Items.map((d: Discipline) =>
            <RowHeader key={d.Id} onClick={() => this.props.handleClick(d)}>
              <Grid item xs={12} container wrap='nowrap' zeroMinWidth>
                <Typography noWrap variant='subtitle1'>
                  {d.Name}
                </Typography>
              </Grid>
            </RowHeader>
          )}
        </BlockContent>
      </Grid>
    </Grid>
  }
}

export default withSnackbar(DisciplineTable) as any