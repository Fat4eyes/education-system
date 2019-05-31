import * as React from 'react'
import {ChangeEvent, Component, ComponentType} from 'react'
import {Collapse, Grid, IconButton, TextField, Typography, withStyles, WithStyles} from '@material-ui/core'
import {Breadcrumbs} from '@material-ui/lab'
import ThemesPageStyles from './ThemesPageStyles'
import DisciplineTable from '../../Table/DisciplineTable'
import Discipline from '../../../models/Discipline'
import {inject} from '../../../infrastructure/di/inject'
import IDisciplineService from '../../../services/DisciplineService'
import Theme from '../../../models/Theme'
import {IPagingOptions} from '../../../models/PagedData'
import RowHeader from '../../Table/RowHeader'
import AddIcon from '@material-ui/icons/Add'
import EditIcon from '@material-ui/icons/Edit'
import ClearIcon from '@material-ui/icons/Clear'
import IThemeService from '../../../services/ThemeService'
import QuestionTable from '../../Table/QuestionTable'
import {Redirect} from 'react-router'
import Block from '../../Blocks/Block'
import {TNotifierProps, withNotifier} from '../../../providers/NotificationProvider'
import {arrayMove, SortEnd} from 'react-sortable-hoc'
import {SortableArrayContainer, SortableArrayItem} from '../../stuff/SortableArray'
import classNames from 'classnames'
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider'
import {TAuthProps} from '../../../providers/AuthProvider/AuthProviderTypes'
import Modal from '../../stuff/Modal'
import {routes} from '../../Layout/Routes'
import AddButton from '../../stuff/AddButton'
import {MtBlock} from '../../stuff/Margin'

type TProps = WithStyles<typeof ThemesPageStyles> & TNotifierProps & TAuthProps

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
  EditThemeId?: number,
  DeleteThemeId?: number
}


class ThemesPage extends Component<TProps, IState> {
  @inject private DisciplineService?: IDisciplineService
  @inject private ThemeService?: IThemeService

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
    const {data, success} = await this.ThemeService!.getByDisciplineId(this.state.Discipline!.Id!, param)

    if (success && data) {
      data.Items.sort((a, b) => a.Order !== undefined && b.Order !== undefined && a.Order > b.Order ? 1 : -1)

      this.setState({Items: data.Items})
    }
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
      if (!this.state.Theme) return

      const theme = this.state.Theme
      const {data, success} = await this.ThemeService!.add(theme)

      if (success && data) {
        this.props.notifier.success(`Тема "${theme.Name}" успешно добавлена`)
        this.setState({
          ShowAddBlock: false,
          Theme: new Theme(),
          Items: [...this.state.Items, {...theme, Id: data}]
        })
      }

    },
    modal: (id?: number) => () => {
      this.setState({
        DeleteThemeId: id
      })
    },
    delete: async () => {
      const id = this.state.DeleteThemeId

      this.handleTheme.modal()()

      if (id && await this.ThemeService!.delete(id)) {
        this.props.notifier.success(`Тема успешно удалена`)
        this.setState(state => ({
          Items: state.Items.filter((t: Theme) => t.Id !== id)
        }))
      }
    },
    sort: ({oldIndex, newIndex}: SortEnd) => {
      if (oldIndex === newIndex || !this.state.Discipline) return

      const themes = arrayMove(this.state.Items, oldIndex, newIndex)
        .map((theme, index): Theme => ({...theme, Order: index}))

      this.setState(
        {Items: themes},
        async () => {
          if (this.state.Discipline && this.state.Discipline.Id) {
            await this.ThemeService!.setOrder(this.state.Discipline.Id, themes)
          }
        }
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
      if (await this.ThemeService!.update(theme)) {
        this.props.notifier.success(`Тема успешно обновлена`)
        this.setState({
          EditThemeId: undefined
        })
      }
    },
    handleKeyDown: (theme: Theme) => async ({key}: any) => {
      if (key === 'Enter') {
        await this.handleTheme.update(theme)()
      }
    }
  }

  render(): React.ReactNode {
    const {classes, auth: {User}} = this.props
    const disciplineSelected = !!this.state.Discipline
    const isLecturer = User && User.Roles && User.Roles.Lecturer

    if (this.state.NeedRedirect && this.state.SelectedThemeId) {
      const {SelectedQuestionId: id, SelectedThemeId: themeId} = this.state
      return <Redirect to={id ? routes.editQuestion(themeId, id) : routes.createQuestion(themeId)}/>
    }
    
    const BreadcrumbsHeader = () =>
      <Grid item xs={12} className={classes.header} container zeroMinWidth wrap='nowrap' alignItems='center'>
        <Breadcrumbs separator={
          <Typography variant='subtitle1' className={classes.headerText}>
            >
          </Typography>
        }>
          <Typography align='center' noWrap variant='subtitle1'
                      className={classNames(classes.headerText, classes.breadcrumbs, {
                        [classes.breadcrumbsClickable]: disciplineSelected
                      })}
                      onClick={this.handleDisciplinesTableVisible}
          >
            {disciplineSelected ? this.state.Discipline!.Name : 'Дисциплины'}
          </Typography>
          {
            disciplineSelected &&
            <Typography align='center' noWrap variant='subtitle1'
                        className={classNames(classes.headerText, classes.breadcrumbs, {
                          [classes.breadcrumbsClickable]: this.state.SelectedThemeId
                        })}
                        onClick={() => this.handleTheme.select(this.state.SelectedThemeId!)}
            >
              {
                this.state.SelectedThemeId
                  ? this.state.Items.find(t => t.Id === this.state.SelectedThemeId)!.Name
                  : 'Темы'
              }
            </Typography>}
          {
            this.state.SelectedThemeId &&
            <Typography align='center' noWrap variant='subtitle1'
                        className={classNames(classes.headerText, classes.breadcrumbs)}>
              Вопросы
            </Typography>
          }
        </Breadcrumbs>
      </Grid>

    return <Grid container justify='center'>
      <Grid item xs={12}>
        <Block partial>
          <BreadcrumbsHeader/>
          <MtBlock value={this.state.ShowDisciplinesTable ? 3 : 4}/>
          <Grid item xs={12}>
            <Collapse timeout={500} in={this.state.ShowDisciplinesTable}>
              <DisciplineTable handleClick={this.handleChangeDiscipline}/>
            </Collapse>
          </Grid>
          <Grid item xs={12}>
            <Collapse timeout={500} in={!this.state.ShowDisciplinesTable && !this.state.ShowQuestionsBlock}>
              {
                isLecturer && <>
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
                </>
              }
              {
                isLecturer &&
                <SortableArrayContainer onSortEnd={this.handleTheme.sort} useDragHandle>
                  {this.state.Items.map((theme: Theme, index: number) =>
                    <SortableArrayItem key={theme.Id} index={index} value={
                      <>
                        {
                          !this.state.EditThemeId || this.state.EditThemeId !== theme.Id
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
                        <Grid item className={classes.mrUnit}/>
                        <ClearIcon color='action' onClick={this.handleTheme.modal(theme.Id)}/>
                      </>
                    }/>
                  )}
                </SortableArrayContainer>
              }
              {
                !isLecturer &&
                this.state.Items.map((theme: Theme) =>
                  <Grid item xs={12} container key={theme.Id}>
                    <RowHeader>
                      <Grid item xs container zeroMinWidth alignItems='center'
                            onClick={() => this.handleTheme.select(theme.Id!)}
                      >
                        <Typography variant='subtitle1'>
                          {theme.Name}
                        </Typography>
                      </Grid>
                      <ClearIcon color='action' onClick={this.handleTheme.modal(theme.Id)}/>
                    </RowHeader>
                  </Grid>
                )
              }
            </Collapse>
          </Grid>
          {
            this.state.SelectedThemeId && <>
              {
                isLecturer &&
                <Grid item xs={12}>
                  <AddButton onClick={() => this.setState({NeedRedirect: true})}/>
                </Grid>
              }
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
      <Modal
        isOpen={!!this.state.DeleteThemeId}
        onClose={this.handleTheme.modal()}
        onYes={this.handleTheme.delete}
        onNo={this.handleTheme.modal()}
      />
    </Grid>
  }
}

export default withAuthenticated(withNotifier(withStyles(ThemesPageStyles)(ThemesPage))) as ComponentType<{}>