import React, {Component} from 'react'
import {CircularProgress, Grid, Paper, Typography, withStyles} from '@material-ui/core'
import {If, TablePagination} from '../../../core'
import {disciplineRoutes, testRoutes} from '../../../../routes'
import TestsFilter from './TestsFilter'
import {withSnackbar} from 'notistack'
import LinearProgress from '@material-ui/core/LinearProgress'
import {Mapper, ProtectedFetch, Snackbar, UrlBuilder} from '../../../../helpers'
import TestsTableStyles from './TestsTableStyles'
import classNames from 'classnames'
import TestDetails from './Details/TestDetails'
import Collapse from '@material-ui/core/Collapse'

const TestModel = {
  Subject: '',
  TotalTime: '',
  Attempts: '',
  IsActive: '',
  IsSelected: false,
  IsDetailsLoaded: false
}

const minLengthForTrigger = 3

@withStyles(TestsTableStyles)
@withSnackbar
class TestsTable extends Component {
  constructor(props) {
    super(props)

    this.state = {
      Filter: {
        IsActive: false,
        DisciplineId: null,
        Disciplines: [],
        Name: ''
      },
      IsLoading: false,
      IsFirstLoading: true,
      Count: 0,
      CountPerPage: 10,
      Page: 0,
      Items: []
    }
    this.tableRef = React.createRef()
    this.Snackbar = new Snackbar(this.props.enqueueSnackbar)
  }

  async componentDidMount() {
    try {
      let {Items: Disciplines} = await ProtectedFetch.get(
        UrlBuilder.Build(disciplineRoutes.getDisciplines, {All: true})
      )
      let {Items, Count} = await ProtectedFetch.get(
        UrlBuilder.Build(testRoutes.getTests, {
          DisciplineId: this.props.disciplineId,
          OnlyActive: this.props.isActite,
          Skip: 0,
          Take: this.state.CountPerPage
        })
      )
      this.setState({
        Items: Mapper.map(Items, TestModel),
        Count,
        IsFirstLoad: false,
        Filter: {
          ...this.state.Filter,
          Disciplines
        }
      })
    } catch (e) {
      this.Snackbar.Error(e)
    }
  }

  getTests = async (param = {}) => {
    try {
      let {Items, Count} = await ProtectedFetch.get(
        UrlBuilder.Build(testRoutes.getTests, {
          DisciplineId: this.state.Filter.DisciplineId,
          OnlyActive: this.state.Filter.IsActive,
          Name: this.state.Filter.Name.length < 3 ? null : this.state.Filter.Name,
          Skip: 0,
          Take: this.state.CountPerPage,
          ...param
        })
      )
      this.setState({Items: Mapper.map(Items, TestModel), Count: Count, IsLoading: false})
    } catch (e) {
      this.Snackbar.Error(e)
    }
  }

  handleChangePage = async page =>
    this.setState({Page: page, IsLoading: true},
      async () =>
        await this.getTests({
          Skip: page * this.state.CountPerPage,
          Take: this.state.CountPerPage,
          IsLoading: false
        }))

  handleChangeRowsPerPage = ({target: {value}}) => {
    if (value === this.state.CountPerPage) return
    this.setState({
      CountPerPage: value,
      Page: 0,
      IsLoading: true
    }, async () => {
      await this.getTests({
        Skip: 0,
        Take: value,
        IsLoading: false
      })
    })
  }

  handleDatailsClick = id => {
    if (this.state.IsLoading) return
    this.setState({
      Items: this.state.Items.map(t => ({
        ...t,
        IsSelected: t.Id === id ? !t.IsSelected : false,
        IsDetailsLoaded: false
      }))
    })
  }

  handleDetailsLoad = id => {
    this.setState({
      Items: this.state.Items.map(t => ({
        ...t, IsDetailsLoaded: t.Id === id
      }))
    })
  }

  handleFilter = isChecked => ({target: {name, value, checked}}) => {
    if (isChecked) {
      if (checked === this.state.Filter.IsActive)
        return
    } else {
      if (value === this.state.Filter[name] || value === 0 && this.state.Filter[name] === null)
        return
    }

    return this.setState({
      Filter: {
        ...this.state.Filter,
        [name]: (!!isChecked ? checked : (value || null))
      },
      IsLoading: true,
      Page: 0
    }, this.getTests)
  }

  handleSearch = ({target: {name, value}}) => {
    const oldValue = this.state.Filter[name]

    if (value.length >= minLengthForTrigger || value.length < minLengthForTrigger && oldValue.length >= minLengthForTrigger) {
      this.setState({
        Filter: {
          ...this.state.Filter,
          [name]: value || ''
        },
        IsLoading: true,
        Page: 0
      }, this.getTests)
    } else {
      this.setState({
        Filter: {
          ...this.state.Filter,
          [name]: value || ''
        }
      })
    }
  }

  render() {
    let {classes} = this.props
    let {DisciplineId, Disciplines, IsActive, Name} = this.state.Filter

    const getTableHeight = () => {
      if (this.tableRef.current)
        return this.tableRef.current.clientHeight
      else
        return 100
    }

    return <Grid container justify='space-around' className={classes.main} spacing={16}>
      <Grid item xs={12} md={4} lg={3}>
        <TestsFilter handleInput={this.handleFilter}
                     DisciplineId={DisciplineId || 0}
                     Disciplines={Disciplines}
                     IsActive={IsActive}
                     Name={Name}
                     handleSearch={this.handleSearch}
        />
      </Grid>
      <Grid item xs={12} md={8} lg={9}>
        <Paper className={classes.paper}>
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
          <If condition={!this.state.IsFirstLoad} orElse={
            <div style={{height: getTableHeight()}}>
              <div className={classes.loading}>
                <CircularProgress size={100} thickness={1}/>
              </div>
            </div>
          }>
            <div className={classes.loadingBlock}>
              <If condition={this.state.IsLoading}>
                <LinearProgress/>
              </If>
            </div>
            <div ref={this.tableRef} className={classes.tableContainer}>
              <Grid container ref={this.tableRef}>
                <If condition={!!this.state.Items.length} orElse={
                  <Grid item xs={12} color='primary'>
                    <Typography variant='subtitle1' className={classes.titleNotSelected}>
                      {'Не найдено'}
                    </Typography>
                  </Grid>
                }>
                  {this.state.Items.map((test, index) =>
                    <Grid container key={index} className={classes.row}>
                      <Grid item xs={12} container wrap='nowrap' zeroMinWidth
                            className={classNames(classes.rowHeader, {
                              [classes.rowHeaderSelected]: test.IsSelected && test.IsDetailsLoaded
                            })}
                            onClick={() => this.handleDatailsClick(test.Id)}
                      >
                        <Typography noWrap variant='subtitle1'>
                          {test.Subject}
                        </Typography>
                      </Grid>
                      <Grid item xs={12} className={classes.rowProgress}>
                        <If condition={test.IsSelected && !test.IsDetailsLoaded}>
                          <LinearProgress/>
                        </If>
                      </Grid>
                      <Collapse in={test.IsDetailsLoaded} className={classes.collapse}>
                        <TestDetails test={test} handleDetailsLoad={this.handleDetailsLoad}/>
                      </Collapse>
                    </Grid>)}
                </If>
              </Grid>
            </div>
          </If>
          <TablePagination
            count={{
              all: this.state.Count,
              perPage: this.state.CountPerPage,
              current: this.state.Items.length
            }}
            page={this.state.Page}
            onPageChange={this.handleChangePage}
            onCountPerPageChange={this.handleChangeRowsPerPage}
            showChangeCountPerPageBlock={true}
          />
        </Paper>
      </Grid>
    </Grid>
  }
}

export default TestsTable