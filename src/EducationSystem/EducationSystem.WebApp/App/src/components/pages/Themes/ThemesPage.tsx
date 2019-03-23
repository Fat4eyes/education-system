import * as React from 'react'
import {ChangeEvent} from 'react'
import {
  Button,
  Collapse,
  Grid,
  IconButton,
  LinearProgress,
  Paper,
  TextField,
  Typography,
  withStyles,
  WithStyles
} from '@material-ui/core'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import ThemesPageStyles from './ThemesPageStyles'
import DisciplineTable from '../../Table/DisciplineTable'
import Discipline from '../../../models/Discipline'
import {inject} from '../../../infrastructure/di/inject'
import IDisciplineService from '../../../services/abstractions/IDisciplineService'
import TableComponent from '../../Table/TableComponent'
import Theme from '../../../models/Theme'
import {ITableState} from '../../Table/IHandleTable'
import IPagedData, {IPagingOptions} from '../../../models/PagedData'
import {Exception} from '../../../helpers'
import RowHeader from '../../Table/RowHeader'
import {TablePagination} from '../../core'
import AddIcon from '@material-ui/icons/Add'
import ClearIcon from '@material-ui/icons/Clear'
import IThemeService from '../../../services/abstractions/IThemeService'

type TProps = WithStyles<typeof ThemesPageStyles> & InjectedNotistackProps

interface IState extends ITableState<Theme> {
  Discipline?: Discipline,
  ShowDisciplinesTable: boolean,
  ShowAddBlock: boolean,
  Theme?: Theme
}

class ThemesPage extends TableComponent<Theme, TProps, IState> {
  @inject
  private DisciplineService?: IDisciplineService

  @inject
  private ThemeService?: IThemeService

  constructor(props: TProps) {
    super(props)

    this.state = {
      Count: 0,
      CountPerPage: 10,
      Page: 0,
      Items: [],
      IsLoading: false,
      ShowDisciplinesTable: true,
      ShowAddBlock: false,
      Theme: new Theme()
    } as IState
  }

  getTableData = async (param: IPagingOptions = {Skip: 0, Take: this.state.CountPerPage}) => {
    let result = await this.DisciplineService!
      .getThemes(this.state.Discipline!.Id!, param)

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
      Count: (result as IPagedData<Theme>).Count,
      Items: (result as IPagedData<Theme>).Items,
      IsLoading: false
    })
  }

  handleChangeDiscipline = (discipline: Discipline) => {
    this.setState({
      Discipline: discipline,
      Page: 0,
      IsLoading: true
    }, async () => {
      await this.getTableData()
      this.setState({ShowDisciplinesTable: false})
    })
  }

  handleDisciplinesTableVisible = () =>
    this.setState(state => ({
      ShowDisciplinesTable: !state.ShowDisciplinesTable
    }))

  handleTheme = {
    open: () => {
      if (this.state.Discipline) {
        this.setState({ShowAddBlock: true, Theme: new Theme(this.state.Discipline.Id!)})
      }
    },
    close: () => this.setState({ShowAddBlock: false, Theme: new Theme()}),
    change: ({target: {value}}: ChangeEvent<HTMLInputElement> | any) => {
      this.setState(state => ({
        Theme: {
          ...state.Theme,
          Name: value
        }
      }))
    },
    submit: async () => {
      let result = await this.ThemeService!.add(this.state.Theme!)

      if (result instanceof Exception) {
        return this.props.enqueueSnackbar(result.message, {
          variant: 'error',
          anchorOrigin: {
            vertical: 'bottom',
            horizontal: 'right'
          }
        })
      }

      if ((result as Theme).Id) {
        this.props.enqueueSnackbar(`Тема "${result.Name}" успешно добавлена`, {
          variant: 'success',
          anchorOrigin: {
            vertical: 'bottom',
            horizontal: 'right'
          }
        })

        this.setState({ShowAddBlock: false, Theme: new Theme()})
      }
    }
  }

  render(): React.ReactNode {
    let {classes} = this.props

    return <Grid container justify='center'>
      <Grid item xs={12} md={10} lg={8}>
        <Paper className={classes.paper}>
          <Grid item xs={12} container justify='space-between'>
            <Grid item>
              <Typography noWrap variant='subtitle1'>
                {!!this.state.Discipline
                  ? `Дисциплина: ${this.state.Discipline!.Name}`
                  : 'Выбор дисциплины'
                }
              </Typography>
            </Grid>
            <Grid item>
              {!!this.state.Discipline && <Button variant='outlined'
                                                  onClick={this.handleDisciplinesTableVisible}
              >
                Выбрать другую
              </Button>}
            </Grid>
          </Grid>
          <Grid item xs={12} className={classes.rowProgress}>
            {this.state.IsLoading && <LinearProgress/>}
          </Grid>
          <Grid item xs={12}>
            <Collapse timeout={500} in={this.state.ShowDisciplinesTable}>
              <DisciplineTable handleClick={this.handleChangeDiscipline}/>
            </Collapse>
          </Grid>
          <Grid item xs={12}>
            <Collapse timeout={500} in={!this.state.ShowDisciplinesTable}>
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
              <Collapse timeout={500} in={!this.state.ShowAddBlock}>
                <RowHeader onClick={this.handleTheme.open}>
                  <Grid item xs={12} container alignItems='center' justify='center' wrap='nowrap' zeroMinWidth>
                    <AddIcon/>
                    <Typography noWrap variant='subtitle1'>
                      Добавить
                    </Typography>
                  </Grid>
                </RowHeader>
              </Collapse>
              <Collapse timeout={500} in={this.state.ShowAddBlock}>
                <Grid container>
                  <Grid item xs>
                    <TextField
                      label='Название темы'
                      placeholder='Название темы'
                      value={this.state.Theme!.Name}
                      onChange={this.handleTheme.change}
                      style={{margin: '0 5px'}}
                      fullWidth
                      margin='none'
                    />
                  </Grid>
                  <Grid item>
                    <IconButton aria-label="Add" onClick={this.handleTheme.submit}>
                      <AddIcon/>
                    </IconButton>
                  </Grid>
                  <Grid item>
                    <IconButton aria-label="Clear" onClick={this.handleTheme.close}>
                      <ClearIcon/>
                    </IconButton>
                  </Grid>
                </Grid>
              </Collapse>
              {this.state.Items.map((theme: Theme) =>
                <RowHeader key={theme.Id}>
                  <Grid item xs={12} container wrap='nowrap' zeroMinWidth>
                    <Typography noWrap variant='subtitle1'>
                      {theme.Name}
                    </Typography>
                  </Grid>
                </RowHeader>)}
            </Collapse>
          </Grid>
        </Paper>
      </Grid>
    </Grid>
  }
}

export default withSnackbar(withStyles(ThemesPageStyles)(ThemesPage) as any)