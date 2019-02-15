import React, {Component} from 'react'
import {CircularProgress, Grid, IconButton, LinearProgress, Typography, withStyles} from '@material-ui/core'
import {Mapper, ProtectedFetch, UrlBuilder} from '../../../../../helpers'
import {testRoutes} from '../../../../../routes'
import {If} from '../../../../core'
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft'
import ChevronRightIcon from '@material-ui/icons/ChevronRight'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import ExpandLessIcon from '@material-ui/icons/ExpandLess'
import withWidth, {isWidthDown} from '@material-ui/core/withWidth'
import PropTypes from 'prop-types'
import ThemesTableStyles from './ThemesTableStyles'
import classNames from 'classnames'

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
    this.props.handleDetailsLoad(this.state.TestId)
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

    const Pagination = () => <>
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
    </>

    const Progress = () =>
      <div className={classes.progress}>
        <If condition={this.state.IsLoading}>
          <LinearProgress/>
        </If>
      </div>

    return <Grid container className={classes.root}>
      <If condition={!Count && !this.state.IsLoading}>
        <Grid item xs={12} container justify='center' wrap='nowrap' zeroMinWidth>
          <Typography variant='subtitle1' noWrap>
            Тем не найдено
          </Typography>
        </Grid>
      </If>
      <If condition={!!Count}>
        <Grid item xs={2} container justify='center' alignItems='center' direction={isLg ? 'column' : 'row'}>
          <Pagination/>
        </Grid>
        <Grid item xs={10}>
          <Progress/>
          <Grid container>
            {this.state.Items.map((theme, index) =>
              <Grid item xs={12} container wrap='nowrap' zeroMinWidth key={index}
                    className={classNames(classes.row, classes.rowHeader)}>
                <Typography noWrap variant='subtitle1'>
                  {theme.Name}
                </Typography>
              </Grid>
            )}
          </Grid>
          <Progress/>
        </Grid>
      </If>
    </Grid>

  }
}

ThemesTable.propTypes = {
  TestId: PropTypes.number.isRequired
}

export default ThemesTable