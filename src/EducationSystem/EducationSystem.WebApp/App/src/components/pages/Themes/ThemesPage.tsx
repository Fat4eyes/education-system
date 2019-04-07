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
import QuestionTable from '../../Table/QuestionTable'
import QuestionHandling from '../QuestionHandling/QuestionHandling'
import {Redirect} from 'react-router'
import Block from '../../Blocks/Block'

type TProps = WithStyles<typeof ThemesPageStyles> & InjectedNotistackProps

interface IState extends ITableState<Theme> {
  Discipline?: Discipline,
  ShowDisciplinesTable: boolean,
  ShowAddBlock: boolean,
  Theme?: Theme,
  SelectedThemeId?: number,
  SelectedQuestionId?: number,
  ShowQuestionsBlock: boolean,
  NeedRedirect: boolean
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
      ShowQuestionsBlock: false,
      Theme: new Theme(),
      NeedRedirect: false
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
      this.setState({
        ShowDisciplinesTable: false
      })
    })
  }

  handleDisciplinesTableVisible = () =>
    this.setState(state => ({
      ShowDisciplinesTable: !state.ShowDisciplinesTable,
      ShowQuestionsBlock: false,
      SelectedThemeId: undefined
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
        },
        SelectedQuestionId: undefined
      }))
    },
    select: (id: number) => {
      this.setState(state => ({
        SelectedThemeId: state.SelectedThemeId === id ? undefined : id,
        SelectedQuestionId: undefined,
        ShowQuestionsBlock: false,
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

        let themes = this.state.Items
        themes.push(result)
        this.setState(state => ({
          ShowAddBlock: false,
          Theme: new Theme(),
          Items: themes,
          Count: state.Count + 1
        }))
      }
    }
  }

  render(): React.ReactNode {
    let {classes} = this.props
    
    if (this.state.NeedRedirect && this.state.SelectedThemeId) 
      return <Redirect to={`/question/${this.state.SelectedThemeId}` + (this.state.SelectedQuestionId ? `/${this.state.SelectedQuestionId}` : '')}/>

    return <Grid container justify='center' spacing={16}>
      <Grid item xs={12} md={10} lg={8}>
        <Block>
          <Typography noWrap variant='subtitle1'>
            Администрирование тем
          </Typography>
        </Block>
      </Grid>
      <Grid item xs={12} md={10} lg={8}>
        <Block>
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
          <Collapse timeout={500} in={!this.state.ShowQuestionsBlock}>
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
                    <Grid item xs={12} container wrap='nowrap' zeroMinWidth
                          onClick={() => this.handleTheme.select(theme.Id!)}
                    >
                      <Typography noWrap variant='subtitle1'>
                        {theme.Name}
                      </Typography>
                    </Grid>
                  </RowHeader>
                )}
              </Collapse>
            </Grid>
          </Collapse>
        </Block>
      </Grid>
      <Grid item xs={12} md={10} lg={8}>
        <Collapse timeout={500} in={this.state.ShowQuestionsBlock}>
          <Block>
            <Grid item xs={12} container justify='space-between'>
              <Grid item xs>
                <Typography noWrap variant='subtitle1'>
                  {this.state.SelectedThemeId && 
                    `Тема: ${this.state.Items.find(t => t.Id === this.state.SelectedThemeId)!.Name}`
                  }
                </Typography>
              </Grid>
              <Grid item>
                {this.state.SelectedThemeId && 
                  <Button variant='outlined' onClick={() => this.setState({NeedRedirect: true})}>
                    Добавить вопрос
                  </Button>
                }
              </Grid>
              <Grid item>
                {this.state.SelectedThemeId && 
                  <Button variant='outlined' onClick={() => this.handleTheme.select(this.state.SelectedThemeId!)}>
                    Выбрать другую
                  </Button>
                }
              </Grid>
            </Grid>
            {this.state.SelectedThemeId && <QuestionTable 
              themeId={this.state.SelectedThemeId} 
              loadCallback={() => this.setState({ShowQuestionsBlock: true})}
              handleClick={(id: number) => this.setState({SelectedQuestionId: id, NeedRedirect: true})}
            />
            }
          </Block>
        </Collapse>
      </Grid>
    </Grid>
  }
}

export default withSnackbar(withStyles(ThemesPageStyles)(ThemesPage) as any)