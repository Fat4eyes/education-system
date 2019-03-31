import {InjectedNotistackProps, withSnackbar} from 'notistack'
import {ITableState} from './IHandleTable'
import {Grid, Typography} from '@material-ui/core'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import RowHeader from './RowHeader'
import TableComponent from './TableComponent'
import * as React from 'react'
import {inject} from '../../infrastructure/di/inject'
import {Exception} from '../../helpers'
import {TablePagination} from '../core'
import Question from '../../models/Question'
import IThemeService from '../../services/abstractions/IThemeService'

interface IProps extends InjectedNotistackProps {
  themeId: number,
  loadCallback: any
}

class QuestionTable extends TableComponent<Question, IProps, ITableState<Question>> {
  @inject
  private ThemeService?: IThemeService

  constructor(props: IProps) {
    super(props)

    this.state = {
      Count: 0,
      CountPerPage: 10,
      Page: 0,
      Items: [],
      IsLoading: false
    }
  }

  getTableData = async (param: IPagingOptions) => {
    let result = await this.ThemeService!.getQuestions(this.props.themeId, {
      Skip: 0,
      Take: this.state.CountPerPage,
      ...param
    })

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
      Count: (result as IPagedData<Question>).Count,
      Items: (result as IPagedData<Question>).Items,
      IsLoading: false
    }, this.props.loadCallback)
  }

  componentDidMount() {
    this.setState({IsLoading: true}, () => this.getTableData({Skip: 0, Take: this.state.CountPerPage}))
  }

  render() {
    return <Grid item xs={12} container justify='center'>
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
      {this.state.Items.map((question: Question) =>
        <RowHeader key={question.Id}>
          <Grid item xs={12} container wrap='nowrap' zeroMinWidth>
            <Typography noWrap variant='subtitle1'>
              {question.Text}
            </Typography>
          </Grid>
        </RowHeader>
      )}
    </Grid>
  }
}

export default withSnackbar(QuestionTable) as any