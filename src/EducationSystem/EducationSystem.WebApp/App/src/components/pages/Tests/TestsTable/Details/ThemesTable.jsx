import React, {Component} from 'react'
import {
  Grid,
  IconButton,
  LinearProgress,
  Table,
  TableBody,
  TableCell,
  TableRow,
  Typography,
  withStyles
} from '@material-ui/core'
import {Mapper, ProtectedFetch, UrlBuilder} from '../../../../../helpers'
import {testRoutes} from '../../../../../routes'
import {If, Try} from '../../../../core'
import ThemesTableStyles from './ThemesTableStyles'
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft'
import ChevronRightIcon from '@material-ui/icons/ChevronRight'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import ExpandLessIcon from '@material-ui/icons/ExpandLess'
import withWidth, {isWidthDown} from '@material-ui/core/withWidth'
import PropTypes from 'prop-types'

const ThemeModel = {
  Name: '1',
  IsSelected: false
}

@withWidth()
@withStyles(ThemesTableStyles)
class ThemesTable extends Component {
  constructor(props) {
    super(props)

    this.state = {
      TestId: this.props.TestId,
      IsLoading: true,
      Count: 0,
      CountPerPage: 5,
      Page: 0,
      Items: [ThemeModel]
    }
  }

  getThemes = async (param = {}) => {
    try {
      let {Items, Count} = await ProtectedFetch.get(
        UrlBuilder.Build(testRoutes.getThemes(this.state.TestId), {
          Skip: 0,
          Take: this.state.CountPerPage,
          ...param
        })
      )
      this.setState({
        Items: Mapper.map(Items, ThemeModel) || [],
        Count,
        IsLoading: false
      })
    } catch (e) {
      this.Snackbar.Error(e)
    }
  }

  async componentDidMount() {
    await this.getThemes()
  }

  handleChangePage = async page => {
    if (page === this.state.Page) return

    this.setState({Page: page, IsLoading: true},
      async () =>
        await this.getThemes({
          Skip: page * this.state.CountPerPage,
          Take: this.state.CountPerPage,
          IsLoading: false
        }))
  }

  render() {
    const {classes, width} = this.props
    const {Page, Count, CountPerPage, Items: {length: CurrentCount}} = this.state
    const leftPage = Page > 0 ? Page - 1 : 0
    const rightPage = Page + 1 < Count / CountPerPage ? Page + 1 : Page
    const leftPageProps = {
      disabled: CountPerPage > Count,
      onClick: () => Page !== leftPage && this.handleChangePage(leftPage)
    }
    const rightPageProps = {
      disabled: CountPerPage > Count,
      onClick: () => Page !== rightPage && this.handleChangePage(rightPage)
    }
    let isLg = isWidthDown('lg', width)

    return <If condition={!!Count} orElse={
      <Grid item xs={12} container justify='center'>
        <Typography variant='subtitle1'>
          Тем не найдено
        </Typography>
      </Grid>
    }>
      <Grid item xs={2} container justify='center' alignItems='center' direction={isLg ? 'column' : 'row'}>
        <If condition={Count > CountPerPage} orElse={
          <Typography variant='subtitle1'>
            Темы:
          </Typography>
        }>
          <If condition={!isLg} orElse={<>
            <IconButton {...leftPageProps}>
              <ExpandLessIcon/>
            </IconButton>
            <Typography variant='subtitle1'>
              {`${(Page * CountPerPage + CurrentCount)} из ${Count}`}
            </Typography>
            <IconButton {...rightPageProps}>
              <ExpandMoreIcon/>
            </IconButton>
          </>}>
            <IconButton {...leftPageProps}>
              <ChevronLeftIcon/>
            </IconButton>
            <Typography variant='subtitle1'>
              {`${(Page * CountPerPage + CurrentCount)} из ${Count}`}
            </Typography>
            <IconButton {...rightPageProps}>
              <ChevronRightIcon/>
            </IconButton>
          </If>
        </If>
      </Grid>
      <Grid item xs={10} className={classes.root}>
        <Try>
          <div>
            <div className={classes.loadingBlock}>
              <If condition={this.state.IsLoading}>
                <LinearProgress/>
              </If>
            </div>
            <Table className={classes.table}>
              <TableBody>
                {this.state.Items.map((theme, index) =>
                  <TableRow key={index}>
                    <TableCell>
                      <Typography noWrap variant='subtitle1' className={classes.themeHeader}>
                        {theme.Name}
                      </Typography>
                    </TableCell>
                  </TableRow>
                )}
              </TableBody>
            </Table>
          </div>
        </Try>
      </Grid>
    </If>
  }
}

ThemesTable.propTypes = {
  TestId: PropTypes.number.isRequired
}

export default ThemesTable