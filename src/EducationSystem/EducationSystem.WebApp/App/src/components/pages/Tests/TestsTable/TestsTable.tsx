import {TNotifierProps, withNotifier} from '../../../../providers/NotificationProvider'
import * as React from 'react'
import {ChangeEvent, ComponentType} from 'react'
import TableComponent from '../../../Table/TableComponent'
import Test from '../../../../models/Test'
import {getDefaultTableState, ITableState} from '../../../Table/IHandleTable'
import {IPagingOptions} from '../../../../models/PagedData'
import ITestService from '../../../../services/TestService'
import {inject} from '../../../../infrastructure/di/inject'
import Discipline from '../../../../models/Discipline'
import IDisciplineService from '../../../../services/DisciplineService'
import {Collapse, Grid, IconButton, Typography, WithStyles, withStyles} from '@material-ui/core'
import TestsTableStyles from './TestsTableStyles'
import Block from '../../../Blocks/Block'
import {MrBlock, MtBlock} from '../../../stuff/Margin'
import {SimpleLink, TablePagination} from '../../../core'
import RowHeader from '../../../Table/RowHeader'
import {TestType} from '../../../../common/enums'
import EditIcon from '@material-ui/icons/Edit'
import ClearIcon from '@material-ui/icons/Clear'
import BlockHeader from '../../../Blocks/BlockHeader'
import Filter from './Filter'
import {withAuthenticated} from '../../../../providers/AuthProvider/AuthProvider'
import {TAuthProps} from '../../../../providers/AuthProvider/AuthProviderTypes'
import Modal from '../../../stuff/Modal'
import {routes} from '../../../Layout/Routes'

type TProps = TNotifierProps & WithStyles<typeof TestsTableStyles> & TAuthProps

interface IFilterState {
  IsActive: boolean,
  DisciplineId?: number,
  Disciplines: Array<Discipline>,
  Name: string
}

interface IState extends ITableState<Test> {
  Filter: IFilterState,
  DetalizedTestId?: number
  DeletableTestId?: number
}

class TestsTable extends TableComponent<Test, TProps, IState> {
  @inject private TestService?: ITestService
  @inject private DisciplineService?: IDisciplineService

  constructor(props: TProps) {
    super(props)

    this.state = {
      ...getDefaultTableState(),
      Filter: {
        IsActive: false,
        Disciplines: [],
        Name: ''
      }
    }
  }

  async componentDidMount() {
    const {data, success} = await this.DisciplineService!.getAll({All: true})

    if (success && data) {
      this.setState(state => ({
        Filter: {
          ...state.Filter,
          Disciplines: data.Items
        }
      }))
    }

    this.getTableData()
  }

  async getTableData(pagingOptions: IPagingOptions = {Skip: 0, Take: this.state.CountPerPage}, param: any = {}) {
    const {data, success} = await this.TestService!.getAll({
      OnlyActive: this.state.Filter.IsActive,
      DisciplineId: this.state.Filter.DisciplineId,
      ...this.getNameFilter(this.state.Filter.Name),
      ...pagingOptions
    })

    if (success && data) {
      this.setState({...data, ...param})
    }
  }

  handleTest = {
    details: (test: Test) => () => {
      this.setState(state => ({
        DetalizedTestId: test.Id === state.DetalizedTestId ? undefined : test.Id
      }))
    },
    modal: (id?: number) => () => {
      this.setState({
        DeletableTestId: id
      })
    },
    delete: async () => {
      this.handleTest.modal()()

      if (this.state.DeletableTestId && await this.TestService!.delete(this.state.DeletableTestId)) {
        let pagingOptions: IPagingOptions = {
          Skip: this.state.Page * this.state.CountPerPage,
          Take: this.state.CountPerPage
        }

        let page = this.state.Page

        if (this.state.Count === this.state.Page * this.state.CountPerPage + this.state.Items.length) {
          if (this.state.Page === 0) {
            pagingOptions.Skip = 0
            page = 0
          } else if (this.state.Items.length === 1) {
            pagingOptions.Skip = (this.state.Page - 1) * this.state.CountPerPage
          }
        }

        this.getTableData(pagingOptions, {Page: page})
      }
    }
  }

  handleFilter = {
    input: (isChecked: boolean) => ({target: {name, value, checked}}: ChangeEvent<HTMLInputElement>) => {
      if (isChecked) {
        if (checked === this.state.Filter.IsActive)
          return
      } else {
        // @ts-ignore
        if (value === this.state.Filter[name] || (value === 0 && this.state.Filter[name] === null))
          return
      }

      return this.setState({
        Filter: {
          ...this.state.Filter,
          [name]: (!!isChecked ? checked : (value || null))
        },
        Page: 0
      }, this.getTableData)
    },
    search: ({target: {name, value}}: ChangeEvent<HTMLInputElement>) => {
      // @ts-ignore
      const oldValue = this.state.Filter[name]

      this.setState({
        Filter: {
          ...this.state.Filter,
          [name]: value || ''
        },
        Page: 0
      }, this.isNeedToReloadData(value, oldValue) ? this.getTableData : undefined)
    }
  }

  render(): React.ReactNode {
    const {classes, auth: {User}} = this.props

    return <Grid container justify='space-around' className={classes.main} spacing={16}>
      <Grid item xs={12} md={4} lg={3}>
        <Filter
          handleInput={this.handleFilter.input}
          DisciplineId={this.state.Filter.DisciplineId || 0}
          Disciplines={this.state.Filter.Disciplines}
          IsActive={this.state.Filter.IsActive}
          Name={this.state.Filter.Name}
          handleSearch={this.handleFilter.search}
        />
      </Grid>
      <Grid item xs={12} md={8} lg={9}>
        <Block partial>
          <BlockHeader>
            <Typography noWrap variant='subtitle1' component='span'>
              Тесты
            </Typography>
          </BlockHeader>
          <MtBlock value={2}/>
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
          <MtBlock value={2}/>
          {this.state.Items.map((test: Test, index: number) =>
            <Grid item xs={12} container key={test.Id || index}>
              <Grid item xs={12}>
                <RowHeader key={test.Id} onClick={this.handleTest.details(test)}>
                  <Grid item xs={12} container wrap='nowrap' zeroMinWidth>
                    <Typography noWrap variant='subtitle1'>
                      {test.Subject}
                    </Typography>
                  </Grid>
                </RowHeader>
              </Grid>
              <Collapse timeout={500} in={this.state.DetalizedTestId === test.Id} className={classes.collapse}>
                <Grid item xs={12} className={classes.rowDetails} container>
                  <Grid item>
                    <Grid item xs={12}>
                      <Typography noWrap className={classes.rowDetailsHeader}>
                        Название
                      </Typography>
                    </Grid>
                    <Grid item xs={12}>
                      <Typography noWrap variant='subtitle1'>
                        {test.Subject}
                      </Typography>
                    </Grid>
                  </Grid>
                  <MrBlock value={3}/>
                  <Grid item>
                    <Grid item xs={12}>
                      <Typography noWrap className={classes.rowDetailsHeader}>
                        Тип
                      </Typography>
                    </Grid>
                    <Grid item xs={12}>
                      <Typography noWrap variant='subtitle1'>
                        {test.Type === TestType.Teaching && 'Обучение'}
                        {test.Type === TestType.Control && 'Контроль знаний'}
                      </Typography>
                    </Grid>
                  </Grid>
                  <MrBlock value={3}/>
                  <Grid item>
                    <Grid item xs={12}>
                      <Typography noWrap className={classes.rowDetailsHeader}>
                        Доступен для студента
                      </Typography>
                    </Grid>
                    <Grid item xs={12}>
                      <Typography noWrap variant='subtitle1'>
                        {test.IsActive ? 'Да' : 'Нет'}
                      </Typography>
                    </Grid>
                  </Grid>
                  <Grid item xs/>
                  {
                    User && User.Roles && User.Roles.Lecturer &&
                    <Grid item>
                      <IconButton component={props => <SimpleLink to={routes.editTest(test.Id)} {...props}/>}>
                        <EditIcon fontSize='small' color='action'/>
                      </IconButton>
                    </Grid>
                  }
                  <Grid item>
                    <IconButton onClick={this.handleTest.modal(test.Id)}>
                      <ClearIcon fontSize='small' color='action'/>
                    </IconButton>
                  </Grid>
                </Grid>
              </Collapse>
            </Grid>
          )}
        </Block>
      </Grid>
      <Modal
        isOpen={!!this.state.DeletableTestId}
        onClose={this.handleTest.modal()}
        onYes={this.handleTest.delete}
        onNo={this.handleTest.modal()}
      />
    </Grid>
  }
}

export default withAuthenticated(withNotifier(withStyles(TestsTableStyles)(TestsTable))) as ComponentType<{}>