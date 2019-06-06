import {Grid, Typography, WithStyles, withStyles} from '@material-ui/core'
import {TestSelectStyles} from './TestSelectStyles'
import Discipline from '../../../../models/Discipline'
import TableComponent from '../../../Table/TableComponent'
import {getDefaultTableState, ITableState} from '../../../Table/IHandleTable'
import {IPagingOptions} from '../../../../models/PagedData'
import * as React from 'react'
import {Fragment} from 'react'
import Block from '../../../Blocks/Block'
import Test from '../../../../models/Test'
import TestData from '../../../../models/TestData'
import {TablePagination} from '../../../core'
import {inject} from '../../../../infrastructure/di/inject'
import ITestService from '../../../../services/TestService'
import IDisciplineService from '../../../../services/DisciplineService'
import TestBlock from './TestBlock'
import {MtBlock} from '../../../stuff/Margin'

interface IProps {}

type TProps = WithStyles<typeof TestSelectStyles> & IProps

interface ITestsDetail {
  IsOpen: boolean
  Data: TestData
}

interface IState extends ITableState<Discipline> {
  Opened: Array<number>,
  TestsDetails: Map<number, ITestsDetail>
}

class TestSelect extends TableComponent<Discipline, TProps, IState> {
  @inject private TestService?: ITestService
  @inject private DisciplineService?: IDisciplineService

  constructor(props: TProps) {
    super(props)

    this.state = {
      ...getDefaultTableState(),
      Opened: [],
      TestsDetails: new Map()
    }
  }

  componentDidMount(): void {
    this.getTableData()
  }

  getTableData = async (param?: IPagingOptions) => {
    const {data, success} = await this.DisciplineService!.getAll({Skip: 0, Take: this.state.CountPerPage, ...param})

    if (success && data) {
      this.setState({
        Count: data.Count,
        Items: data.Items
      })
    }
  }

  handleOpen = (id: number) => async () => {
    let currentDiscipline = this.state.Items.find(d => d.Id === id)

    if (!currentDiscipline) return

    if (!currentDiscipline.Tests || !currentDiscipline.Tests.length) {
      const {data, success} = await this.TestService!.getByDisciplineId(id, {All: true})

      if (success && data) {
        currentDiscipline.Tests = data.Items
      }
    }

    return this.setState(state => ({
      Opened: state.Opened.find(_id => _id === id)
        ? state.Opened.filter(_id => _id !== id)
        : [...state.Opened, id]
    }))
  }
  
  handleResetTestPropcess = async (test: Test) => {
    if (await this.TestService!.resetProcess(test.Id!)) {
      test.PassedQuestionsCount = test.PassedThemesCount = 0
      this.forceUpdate()
    } 
  }

  render(): React.ReactNode {
    let {classes} = this.props

    return <Grid container justify='center'>
      <Grid item xs={12}>
        <Block partial>
          {
            !!this.state.Items.length || <Grid item xs={12} container zeroMinWidth wrap='nowrap' justify='center'>
              <Typography align='center' noWrap color='inherit'>
                Не найдено
              </Typography>
            </Grid>
          }
          {
            this.state.Items.map((discipline: Discipline, index: number) =>
              <Fragment key={discipline.Id || index}>
                <Grid item xs={12}
                      className={classes.header}
                      container
                      zeroMinWidth
                      wrap='nowrap'
                      justify='center'
                      onClick={this.handleOpen(discipline.Id!)}
                >
                  <Typography align='center' noWrap color='inherit'>
                    {discipline.Name || discipline.Abbreviation}
                  </Typography>
                </Grid>
                {
                  this.state.Opened.find(id => id === discipline.Id!) && discipline.Tests &&
                  <Grid item xs={12} className={classes.body}>
                    <Grid container className={classes.mainBodyBlock}>
                      {discipline.Tests.map((test: Test, index: number) =>
                        <Fragment key={test.Id!}>
                          <TestBlock test={test} resetTestProcess={this.handleResetTestPropcess}/>
                          {index < discipline.Tests.length - 1 && <MtBlock value={2}/>}
                        </Fragment>
                      )}
                    </Grid>
                  </Grid>
                }
                {index !== this.state.Items.length - 1 && <MtBlock value={2}/>}
              </Fragment>
            )
          }
          {
            this.state.Count > this.state.CountPerPage &&
            <>
              <MtBlock value={2}/>
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
        </Block>
      </Grid>
    </Grid>
  }
}

export default withStyles(TestSelectStyles)(
  TestSelect
) as any