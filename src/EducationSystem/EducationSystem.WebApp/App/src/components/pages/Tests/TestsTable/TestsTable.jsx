import React, {Component} from 'react'
import {CircularProgress, Grid, Paper, Table, TableBody, TableCell, Typography, withStyles} from '@material-ui/core'
import TestsTableStyles from './TestsTableStyles'
import TableRow from '@material-ui/core/TableRow'
import If from '../../../If'
import TablePagination from '../../../stuff/TablePagination'
import {TestModel} from '../models'
import ProtectedFetch from '../../../../helpers/ProtectedFetch'
import UrlBuilder from '../../../../helpers/UrlBuilder'
import {disciplineRoutes, testRoutes} from '../../../../routes'
import Mapper from '../../../../helpers/Mapper'
import TestsFilter from './TestsFilter'
import {withSnackbar} from 'notistack'

@withStyles(TestsTableStyles)
@withSnackbar
class TestsTable extends Component {
  constructor(props) {
    super(props)

    this.state = {
      Filter: {
        IsActive: false,
        DisciplineId: null,
        Disciplines: []
      },
      IsLoading: true,
      Count: 0,
      CountPerPage: 10,
      Page: 0,
      Items: [TestModel]
    }
    this.tableRef = React.createRef()
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
        IsLoading: false,
        Filter: {
          ...this.state.Filter,
          Disciplines
        }
      })
    } catch (e) {
      this.props.enqueueSnackbar(e, {
        variant: 'error',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })
    }
  }

  getTests = async (param = {}) => {
    try {
      let {Items, Count} = await ProtectedFetch.get(
        UrlBuilder.Build(testRoutes.getTests, {
          DisciplineId: this.state.Filter.DisciplineId,
          OnlyActive: this.state.Filter.IsActive,
          Skip: 0,
          Take: this.state.CountPerPage,
          ...param
        })
      )
      this.setState({Items: Mapper.map(Items, TestModel), Count: Count, IsLoading: false})
    } catch (e) {
      this.props.enqueueSnackbar(e, {
        variant: 'error',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })
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
    this.setState({
      Items: this.state.Items.map(t => ({
        ...t, IsSelected: t.Id === id ? !t.IsSelected : false
      }))
    })
  }

  handleFilter = isChecked => ({target: {name, value, checked}}) =>
    this.setState({
      Filter: {
        ...this.state.Filter,
        [name]: (!!isChecked ? checked : (value || null))
      },
      IsLoading: true,
      Page: 0
    }, this.getTests)

  render() {
    let {classes} = this.props
    let {DisciplineId, Disciplines, IsActive} = this.state.Filter

    const getTableHeight = () => {
      if (this.tableRef.current)
        return this.tableRef.current.clientHeight
      else
        return 100
    }

    return <Grid container justify='space-around' className={classes.main} spacing={16}>
      <Grid item xs={12} lg={3}>
        <TestsFilter handleInput={this.handleFilter}
                     DisciplineId={DisciplineId || 0}
                     Disciplines={Disciplines}
                     IsActive={IsActive}
        />
      </Grid>
      <Grid item xs={12} lg={9}>
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
          <If condition={!this.state.IsLoading} orElse={
            <div style={{height: getTableHeight()}}>
              <div className={classes.loading}>
                <CircularProgress size={100} thickness={1}/>
              </div>
            </div>
          }>
            <div ref={this.tableRef} className={classes.tableContainer}>
              <Table className={classes.root} ref={this.tableRef}>
                <TableBody>
                  <If condition={!!this.state.Items.length} orElse={
                    <TableRow color='primary'>
                      <TableCell>
                        <Typography variant='subtitle1' className={classes.titleNotSelected}>
                          {'Не найдено'}
                        </Typography>
                      </TableCell>
                    </TableRow>
                  }>
                    {this.state.Items.map((test, index) =>
                      <React.Fragment key={test.Id || index}>
                        <TableRow hover selected={test.IsSelected} classes={{selected: classes.selected}} color='primary'
                                  onClick={() => this.handleDatailsClick(test.Id)}>
                          <TableCell>
                            <Typography variant='subtitle1'
                                        className={test.IsSelected ? classes.titleSelected : classes.titleNotSelected}>
                              {test.Subject}
                            </Typography>
                          </TableCell>
                        </TableRow>
                        <If condition={test.IsSelected}>
                          <TableRow className={classes.testDetails}>
                            <TableCell>
                              {test.Subject}
                            </TableCell>
                          </TableRow>
                        </If>
                      </React.Fragment>)}
                  </If>
                </TableBody>
              </Table>
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