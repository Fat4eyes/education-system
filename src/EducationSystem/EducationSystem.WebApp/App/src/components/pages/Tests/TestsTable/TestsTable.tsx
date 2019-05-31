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
import {Collapse, Divider, Grid, Tooltip, Typography, WithStyles, withStyles} from '@material-ui/core'
import TestsTableStyles from './TestsTableStyles'
import Block, {PBlock} from '../../../Blocks/Block'
import {MrBlock, MtBlock} from '../../../stuff/Margin'
import {TablePagination} from '../../../core'
import RowHeader from '../../../Table/RowHeader'
import {TestType} from '../../../../common/enums'
import ClearIcon from '@material-ui/icons/Clear'
import EditIcon from '@material-ui/icons/Edit'
import BlockHeader from '../../../Blocks/BlockHeader'
import Filter from './Filter'
import {withAuthenticated} from '../../../../providers/AuthProvider/AuthProvider'
import {TAuthProps} from '../../../../providers/AuthProvider/AuthProviderTypes'
import Modal from '../../../stuff/Modal'
import {IsMobileAsFuncChild} from '../../../stuff/OnMobile'
import {routes} from '../../../Layout/Routes'
import {Redirect} from 'react-router'
import AddButton from '../../../stuff/AddButton'
import ReplyIcon from '@material-ui/icons/Reply'
import {testingSystemRoutes} from '../../../../routes'

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
  RedirectTo?: string
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
          [name]: (isChecked ? checked : (value || null))
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

  handleRedirect = (id: number) => this.setState({RedirectTo: routes.editTest(id)})

  render(): React.ReactNode {
    const {classes, auth: {User}} = this.props

    if (this.state.RedirectTo) return <Redirect to={this.state.RedirectTo}/>

    const DividerInternal = () => <Divider className={classes.divider}/>

    const TestTableInternal = ({isMobile}: { isMobile: boolean }) => <>
      <BlockHeader>
        <Typography noWrap variant='subtitle1' component='span'>
          Тесты
        </Typography>
        <Grid item xs/>
        <Tooltip title='Система тестирования – Тесты' placement='bottom-start'>
          <a href={testingSystemRoutes.tests()} target='_blank'>
            <ReplyIcon color='action' className={classes.colorWhite}/>
          </a>
        </Tooltip>
      </BlockHeader>
      <MtBlock value={3}/>
      <PBlock left={isMobile}>
        <Filter
          handleInput={this.handleFilter.input}
          DisciplineId={this.state.Filter.DisciplineId || 0}
          Disciplines={this.state.Filter.Disciplines}
          IsActive={this.state.Filter.IsActive}
          Name={this.state.Filter.Name}
          handleSearch={this.handleFilter.search}
          IsMobile={isMobile}
        />
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
      </PBlock>
      <MtBlock value={isMobile ? 4 : 1}/>
      {
        User && User.Roles && User.Roles.Lecturer &&
        <Grid item xs={12}>
          <AddButton onClick={() => this.setState({RedirectTo: routes.createTest})}/>
        </Grid>
      }
      {this.state.Items.map((test: Test, index: number) =>
        <Grid item xs={12} container key={test.Id || index}>
          <Grid item xs={12}>
            <RowHeader key={test.Id} onClick={this.handleTest.details(test)} alignItems='center'>
              <Grid item xs container wrap='nowrap' zeroMinWidth>
                <Typography noWrap variant='subtitle1'>
                  {test.Subject}
                </Typography>
              </Grid>
              <Grid item xs={1}/>
              {
                User && User.Roles && User.Roles.Lecturer && <>
                  <EditIcon fontSize='small' color='action' onClick={() => this.handleRedirect(test.Id!)}/>
                  <MrBlock/>
                </>
              }
              <ClearIcon color='action' onClick={this.handleTest.modal(test.Id)}/>
            </RowHeader>
          </Grid>
          <Collapse timeout={500} in={this.state.DetalizedTestId === test.Id} className={classes.collapse}>
            <Grid item xs={12} className={classes.rowDetails} container alignItems='center'>
              <Grid item xs={3}>
                <Typography className={classes.rowDetailsHeader}>
                  Название
                </Typography>
              </Grid>
              <Grid item xs={8}>
                <Typography>
                  {test.Subject}
                </Typography>
              </Grid>
              <DividerInternal/>
              <Grid item xs={3}>
                <Typography className={classes.rowDetailsHeader}>
                  Тип
                </Typography>
              </Grid>
              <Grid item xs={8}>
                <Typography>
                  {test.Type === TestType.Teaching && 'Обучение'}
                  {test.Type === TestType.Control && 'Контроль знаний'}
                </Typography>
              </Grid>
              <DividerInternal/>
              <Grid item xs={3}>
                <Typography className={classes.rowDetailsHeader}>
                  Активен
                </Typography>
              </Grid>
              <Grid item xs={8}>
                <Typography>
                  {test.IsActive ? 'Да' : 'Нет'}
                </Typography>
              </Grid>
              <DividerInternal/>
            </Grid>
          </Collapse>
        </Grid>
      )}
    </>

    return <Grid container justify='space-around' className={classes.main}>
      <Grid item xs={12} lg={12} xl={12} className={classes.testsBlock}>
        <IsMobileAsFuncChild>
          {(isMobile: boolean) =>
            <Block partial={!isMobile} empty={isMobile} topBot={isMobile}>
              <TestTableInternal isMobile={isMobile}/>
            </Block>
          }
        </IsMobileAsFuncChild>
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