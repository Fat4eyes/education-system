import {Grid, Typography, WithStyles, withStyles} from '@material-ui/core'
import {TNotifierProps, withNotifier} from '../../../../providers/NotificationProvider'
import {TestSelectStyles} from './TestSelectStyles'
import {inject} from '../../../../infrastructure/di/inject'
import IStudentService from '../../../../services/abstractions/IStudentService'
import Discipline from '../../../../models/Discipline'
import {Exception} from '../../../../helpers'
import TableComponent from '../../../Table/TableComponent'
import {getDefaultTableState, ITableState} from '../../../Table/IHandleTable'
import IPagedData, {IPagingOptions} from '../../../../models/PagedData'
import * as React from 'react'
import {Fragment} from 'react'
import Block from '../../../Blocks/Block'
import Test from '../../../../models/Test'
import TestData from '../../../../models/TestData'
import {TablePagination} from '../../../core'

interface IProps {}

type TProps = WithStyles<typeof TestSelectStyles> & TNotifierProps & IProps

interface ITestsDetail {
  IsOpen: boolean
  Data: TestData
}

interface IState extends ITableState<Discipline> {
  Opened: Array<number>,
  TestsDetails: Map<number, ITestsDetail>
}

class TestSelect extends TableComponent<Discipline, TProps, IState> {
  @inject private StudentService?: IStudentService

  constructor(props: TProps) {
    super(props)

    this.state = {
      ...getDefaultTableState(),
      Opened: [],
      TestsDetails: new Map()
    }
  }

  componentDidMount() {
    this.setState({IsLoading: true}, this.getTableData)
  }

  getTableData = async (param?: IPagingOptions) => {
    let result = await this.StudentService!.getDisciplines({
      Skip: 0,
      Take: this.state.CountPerPage,
      ...param
    }, {WithTests: true})

    if (result instanceof Exception)
      return this.props.notifier.error(result.message)

    let map = new Map()

    result.Items.forEach((discipline: Discipline) =>
      discipline.Tests.forEach(async ({Id}: Test) => {
        let result = await this.StudentService!.getTestData(Id!)

        if (result instanceof Exception)
          return this.props.notifier.error(result.message)

        map.set(Id!, {
          IsOpen: true,
          Data: result as TestData
        })
      })
    )
    
    this.setState({
      Count: (result as IPagedData<Discipline>).Count,
      Items: (result as IPagedData<Discipline>).Items,
      IsLoading: false,
      TestsDetails: map
    })
  }

  handleOpen = (id: number) => () =>
    this.setState(state => ({
      Opened: state.Opened.find(_id => _id === id)
        ? state.Opened.filter(_id => _id !== id)
        : [...state.Opened, id]
    }))

  handleTestDetails = (test: Test) => async () => {
    let data = this.state.TestsDetails.get(test.Id!)

    let map = new Map(this.state.TestsDetails)

    if (data) {
      map.delete(test.Id!)
      map.set(test.Id!, {
        IsOpen: !data.IsOpen,
        Data: data.Data
      })
    } else {
      let result = await this.StudentService!.getTestData(test.Id!)

      if (result instanceof Exception)
        return this.props.notifier.error(result.message)

      map.set(test.Id!, {
        IsOpen: true,
        Data: result as TestData
      })
    }

    this.setState({
      TestsDetails: map
    })
  }

  render(): React.ReactNode {
    let {classes} = this.props


    return <Grid container justify='center'>
      <Grid item xs={12} md={10} lg={8}>
        <Block partial>
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
                    <Grid item xs={12} className={classes.mt2Unit}/>
                    <Grid container className={classes.mainBodyBlock} spacing={16}>
                      {
                        discipline.Tests.map((test: Test) =>
                          <Grid item xs={4} key={test.Id!}>
                            <Block partial>
                              <Grid container className={classes.bodyBlock}>
                                <Grid item xs={12} container zeroMinWidth wrap='nowrap'>
                                  <Typography align='center' noWrap color='inherit'>
                                    <b>{test.Subject}</b>
                                  </Typography>
                                </Grid>
                                {(() => {
                                    let data = this.state.TestsDetails.get(test.Id!)
                                    return data && data.IsOpen && <>
                                      <Grid item xs={12} className={classes.mt2Unit}/>
                                      <Grid item xs={12} container zeroMinWidth wrap='nowrap'>
                                        <Typography align='center' noWrap color='inherit'>
                                          Количество вопросов: {data.Data.QuestionsCount}
                                        </Typography>
                                      </Grid>
                                      <Grid item xs={12} container zeroMinWidth wrap='nowrap'>
                                        <Typography align='center' noWrap color='inherit'>
                                          Количество тем: {data.Data.ThemesCount}
                                        </Typography>
                                      </Grid>
                                    </>
                                  }
                                )()}
                              </Grid>
                            </Block>
                          </Grid>
                        )
                      }
                    </Grid>
                  </Grid>
                }
                {index !== this.state.Items.length - 1 && <Grid item xs={12} className={classes.mt2Unit}/>}
              </Fragment>
            )
          }
          {
            this.state.Count > this.state.CountPerPage &&
            <>
              <Grid item xs={12} className={classes.mt2Unit}/>
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

export default withStyles(TestSelectStyles)(withNotifier(TestSelect)) as any