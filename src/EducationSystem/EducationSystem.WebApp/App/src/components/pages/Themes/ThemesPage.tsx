import * as React from 'react'
import {ChangeEvent, Component, ComponentType} from 'react'
import {Collapse, Grid, IconButton, TextField, Typography, withStyles, WithStyles} from '@material-ui/core'
import {InjectedNotistackProps} from 'notistack'
import ThemesPageStyles from './ThemesPageStyles'
import DisciplineTable from '../../Table/DisciplineTable'
import Discipline from '../../../models/Discipline'
import {inject} from '../../../infrastructure/di/inject'
import IDisciplineService from '../../../services/abstractions/IDisciplineService'
import Theme from '../../../models/Theme'
import IPagedData, {IPagingOptions} from '../../../models/PagedData'
import RowHeader from '../../Table/RowHeader'
import AddIcon from '@material-ui/icons/Add'
import EditIcon from '@material-ui/icons/Edit'
import ClearIcon from '@material-ui/icons/Clear'
import IThemeService from '../../../services/abstractions/IThemeService'
import QuestionTable from '../../Table/QuestionTable'
import {Redirect} from 'react-router'
import Block from '../../Blocks/Block'
import {resultHandler, voidHandler} from '../../../helpers/Exception'
import {TNotifierProps, withNotifier} from '../../../providers/NotificationProvider'
import {arrayMove, SortEnd} from 'react-sortable-hoc'
import {SortableArrayContainer, SortableArrayItem} from '../../stuff/SortableArray'

type TProps = WithStyles<typeof ThemesPageStyles> & InjectedNotistackProps & TNotifierProps

interface IState {
  Items: Array<Theme>
  Discipline?: Discipline,
  ShowDisciplinesTable: boolean,
  ShowAddBlock: boolean,
  Theme?: Theme,
  SelectedThemeId?: number,
  SelectedQuestionId?: number,
  ShowQuestionsBlock: boolean,
  NeedRedirect: boolean,
  EditThemeId?: number
}

class ThemesPage extends Component<TProps, IState> {
  @inject private DisciplineService?: IDisciplineService
  @inject private ThemeService?: IThemeService
  private _resultHandler = resultHandler.onError(this.props.notifier.error)

  constructor(props: TProps) {
    super(props)

    this.state = {
      Items: [],
      ShowDisciplinesTable: true,
      ShowAddBlock: false,
      ShowQuestionsBlock: false,
      Theme: new Theme(),
      NeedRedirect: false
    } as IState
  }

  getTableData = async (param: IPagingOptions = {All: true}) => {
    this._resultHandler
      .onSuccess((data: IPagedData<Theme>) => this.setState({Items: data.Items}))
      .handleResult(await this.DisciplineService!.getThemes(this.state.Discipline!.Id!, param))
  }

  handleChangeDiscipline = (discipline: Discipline) => {
    this.setState({
      Discipline: discipline
    }, async () => {
      await this.getTableData()
      this.setState({
        ShowDisciplinesTable: false
      })
    })
  }

  handleDisciplinesTableVisible = () => {
    if (!this.state.Discipline) return

    return this.setState(state => ({
      ShowDisciplinesTable: !state.ShowDisciplinesTable,
      ShowQuestionsBlock: false,
      SelectedThemeId: undefined,
      Discipline: undefined
    }))
  }

  handleTheme = {
    open: () => this.state.Discipline && this.setState({
      ShowAddBlock: true,
      Theme: new Theme(this.state.Discipline.Id!)
    }),
    close: () => this.setState({ShowAddBlock: false, Theme: new Theme()}),
    change: ({target: {value}}: ChangeEvent<HTMLInputElement> | any) =>
      this.setState(state => ({
        Theme: {...state.Theme, Name: value},
        SelectedQuestionId: undefined
      })),
    select: (id: number) => {
      return this.setState(state => ({
        SelectedThemeId: state.SelectedThemeId === id ? undefined : id,
        SelectedQuestionId: undefined,
        ShowQuestionsBlock: false
      }))
    },
    submit: async () => {
      this._resultHandler
        .onSuccess((result: Theme) => {
          if (result.Id) {
            this.props.notifier.success(`Тема "${result.Name}" успешно добавлена`)
            this.setState({
              ShowAddBlock: false,
              Theme: new Theme(),
              Items: [...this.state.Items, result as Theme]
            })
          }
        })
        .handleResult(await this.ThemeService!.add(this.state.Theme!))
    },
    delete: async (theme: Theme) => {
      voidHandler
        .onError(this.props.notifier.error)
        .onSuccess(() => {
          this.props.notifier.success(`Тема успешно удалена`)
          this.setState(state => ({
            Items: state.Items.filter((t: Theme) => t.Id !== theme.Id)
          }))
        })
        .handleResult(await this.ThemeService!.delete(theme.Id!))
    },
    sort: async ({oldIndex, newIndex}: SortEnd) => {
      if (oldIndex === newIndex) return 
      
      const themes = arrayMove(this.state.Items, oldIndex, newIndex)
      voidHandler
        .onError(this.props.notifier.error)
        .onSuccess(() => this.setState({Items: themes}))
        .handleResult(await this.DisciplineService!
          .updateDisciplineThemes(this.state.Discipline!.Id!, this.state.Items)
        )
    },
    changeName: (theme: Theme) => ({target: {value}}: ChangeEvent<HTMLInputElement>) => {
      theme.Name = value
      this.forceUpdate()
    },
    edit: (id: number) => () => {
      this.setState({
        EditThemeId: id
      })
    },
    update: (theme: Theme) => async () => {
      this._resultHandler
        .onSuccess((result: Theme) => {
          if (result.Id) {
            this.props.notifier.success(`Тема успешно обновлена`)
            this.setState({
              EditThemeId: undefined
            })
          }
        })
        .handleResult(await this.ThemeService!.update(theme))
    },
    handleKeyDown: (theme: Theme) => async ({key}: any) => {
      if (key === 'Enter') {
        await this.handleTheme.update(theme)()
      }
    }
  }

  render(): React.ReactNode {
    const {classes} = this.props
    const disciplineSelected = !!this.state.Discipline

    if (this.state.NeedRedirect && this.state.SelectedThemeId)
      return <Redirect to={
        `/question/${this.state.SelectedThemeId}` +
        (this.state.SelectedQuestionId ? `/${this.state.SelectedQuestionId}` : '')
      }/>

    const AddButton = ({onClick}: { onClick: () => void }) => <RowHeader onClick={onClick}>
      <Grid item xs={12} container alignItems='center' justify='center' wrap='nowrap' zeroMinWidth>
        <AddIcon/>
        <Typography noWrap variant='subtitle1'>
          Добавить
        </Typography>
      </Grid>
    </RowHeader>

    const DisciplineHeader = () => <>
      <Grid item xs={12} className={classes.header} onClick={this.handleDisciplinesTableVisible}
            container zeroMinWidth wrap='nowrap' justify='center' alignItems='center'>
        <Typography align='center' noWrap color='inherit' variant='subtitle1'>
          {disciplineSelected ? `Дисциплина: ${this.state.Discipline!.Name}` : 'Дисциплины'}
        </Typography>
      </Grid>
    </>

    const ThemeHeader = () => <>
      <Grid item xs={12} className={classes.mt2Unit}/>
      <Grid item xs={12} className={classes.header} onClick={() => this.handleTheme.select(this.state.SelectedThemeId!)}
            container zeroMinWidth wrap='nowrap' justify='center' alignItems='center'>
        <Typography align='center' noWrap color='inherit' variant='subtitle1'>
          {
            this.state.SelectedThemeId
              ? `Тема: ${this.state.Items.find(t => t.Id === this.state.SelectedThemeId)!.Name}`
              : 'Темы'
          }
        </Typography>
      </Grid>
      <Grid item xs={12} className={classes.mt2Unit}/>
    </>

    const QuestionHeader = () => <>
      <Grid item xs={12} className={classes.header}
            container zeroMinWidth wrap='nowrap' justify='center' alignItems='center'>
        <Typography align='center' noWrap color='inherit' variant='subtitle1'>
          Вопросы
        </Typography>
      </Grid>
      <Grid item xs={12} className={classes.mt2Unit}/>
    </>

    return <Grid container justify='center' spacing={16}>
      <Grid item xs={12} md={10} lg={8}>
        <Block partial>
          <DisciplineHeader/>
          <Grid item xs={12}>
            <Collapse timeout={500} in={this.state.ShowDisciplinesTable}>
              <Grid item xs={12} className={classes.mt2Unit}/>
              <DisciplineTable handleClick={this.handleChangeDiscipline}/>
            </Collapse>
          </Grid>
          {disciplineSelected && <ThemeHeader/>}
          <Grid item xs={12}>
            <Collapse timeout={500} in={!this.state.ShowDisciplinesTable && !this.state.ShowQuestionsBlock}>
              <Collapse timeout={500} in={!this.state.ShowAddBlock}>
                <AddButton onClick={this.handleTheme.open}/>
              </Collapse>
              <Collapse timeout={500} in={this.state.ShowAddBlock}>
                <Grid container>
                  <Grid item xs>
                    <TextField
                      autoFocus
                      label='Название темы'
                      placeholder='Название темы'
                      value={this.state.Theme!.Name}
                      onChange={this.handleTheme.change}
                      fullWidth
                      margin='none'
                    />
                  </Grid>
                  <Grid item>
                    <IconButton onClick={this.handleTheme.submit}>
                      <AddIcon/>
                    </IconButton>
                  </Grid>
                  <Grid item>
                    <IconButton onClick={this.handleTheme.close}>
                      <ClearIcon/>
                    </IconButton>
                  </Grid>
                </Grid>
              </Collapse>
              <SortableArrayContainer onSortEnd={this.handleTheme.sort} useDragHandle>
                {this.state.Items.map((theme: Theme, index: number) =>
                  <SortableArrayItem key={theme.Id} index={index} value={
                    <>
                      {
                        this.state.EditThemeId !== theme.Id
                          ? <>
                            <Grid item xs container zeroMinWidth alignItems='center'
                                  onClick={() => this.handleTheme.select(theme.Id!)}
                            >
                              <Typography variant='subtitle1'>
                                {theme.Name}
                              </Typography>
                            </Grid>
                            <EditIcon color='action' onClick={this.handleTheme.edit(theme.Id!)} fontSize='small'/>
                          </>
                          : <>
                            <Grid item xs container wrap='nowrap' zeroMinWidth alignItems='center'
                                  onKeyDown={this.handleTheme.handleKeyDown(theme)}
                            >
                              <TextField
                                autoFocus
                                value={theme.Name}
                                onChange={this.handleTheme.changeName(theme)}
                                fullWidth
                                margin='none'
                              />
                            </Grid>
                            <AddIcon color='action' onClick={this.handleTheme.update(theme)}/>
                          </>
                      }
                    </>
                  }/>
                )}
              </SortableArrayContainer>
            </Collapse>
          </Grid>
          {
            this.state.SelectedThemeId && <>
              <QuestionHeader/>
              <Grid item xs={12}>
                <AddButton onClick={() => this.setState({NeedRedirect: true})}/>
              </Grid>
              <Grid item xs={12}>
                <Collapse timeout={500} in={this.state.ShowQuestionsBlock}>
                  {
                    this.state.SelectedThemeId &&
                    <QuestionTable
                      themeId={this.state.SelectedThemeId}
                      loadCallback={() => this.setState({ShowQuestionsBlock: true})}
                      handleClick={(id: number) => this.setState({SelectedQuestionId: id, NeedRedirect: true})}
                    />
                  }
                </Collapse>
              </Grid>
            </>
          }
        </Block>
      </Grid>
    </Grid>
  }
}

export default withNotifier(withStyles(ThemesPageStyles)(ThemesPage)) as ComponentType<{}>