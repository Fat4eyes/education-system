import React, {Component} from 'react'
import {Grid, Paper, Table, TableBody, TableCell, Typography, withStyles} from '@material-ui/core'
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

@withStyles(TestsTableStyles)
//@withSnackbar
class TestsTable extends Component {
  constructor(props) {
    super(props)

    this.state = {
      Filter: {
        IsActive: false,
        DisciplineId: null,
        Disciplines: []
      },
      Count: 0,
      CountPerPage: 10,
      Page: 0,
      Items: [TestModel]
    }
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
      this.setState({Items: Mapper.map(Items, TestModel), Count: Count})
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
    this.setState({Page: page},
      async () =>
        await this.getTests({
          Skip: page * this.state.CountPerPage,
          Take: this.state.CountPerPage
        }))

  handleChangeRowsPerPage = ({target: {value}}) => {
    if (value === this.state.CountPerPage) return
    this.setState({
      CountPerPage: value,
      Page: 0
    }, async () => {
      await this.getTests({
        Skip: 0,
        Take: value
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
      Page: 0
    }, this.getTests)

  render() {
    let {classes} = this.props
    let {DisciplineId, Disciplines, IsActive} = this.state.Filter

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
          <Table className={classes.root}>
            <TableBody>
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
            </TableBody>
          </Table>
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