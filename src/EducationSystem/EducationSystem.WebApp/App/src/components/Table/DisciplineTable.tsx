import {InjectedNotistackProps, withSnackbar} from 'notistack'
import {ITableState} from './IHandleTable'
import {FormControl, Grid, InputLabel, Typography} from '@material-ui/core'
import {IPagingOptions} from '../../models/PagedData'
import RowHeader from './RowHeader'
import TableComponent from './TableComponent'
import IDisciplineService from '../../services/DisciplineService'
import * as React from 'react'
import {ChangeEvent} from 'react'
import Discipline from '../../models/Discipline'
import {inject} from '../../infrastructure/di/inject'
import {TablePagination} from '../core'
import Input from '../stuff/Input'
import {MtBlock} from '../stuff/Margin'
import {PBlock} from '../Blocks/Block'
import {IsMobileAsFuncChild} from '../stuff/OnMobile'

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
      CountPerPage: 20,
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
        <IsMobileAsFuncChild>
          {(isMobile: boolean) =>
            <PBlock left={isMobile}>
              <FormControl fullWidth>
                <InputLabel shrink htmlFor='Name'>
                  Название дисциплины:
                </InputLabel>
                <Input
                  value={this.state.Name}
                  name='Name'
                  onChange={this.handleName}
                  fullWidth
                />
              </FormControl>
            </PBlock>
          }
        </IsMobileAsFuncChild>
      </Grid>
      {
        this.state.Count > this.state.Items.length && <>
          <MtBlock value={4}/>
          <Grid item xs={12}>
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
          </Grid>
        </>
      }
      <MtBlock value={4}/>
      <Grid item xs={12}>
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
    </Grid>
  }
}

export default withSnackbar(DisciplineTable) as any