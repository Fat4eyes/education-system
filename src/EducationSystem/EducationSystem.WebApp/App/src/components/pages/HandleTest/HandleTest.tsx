import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import Grid from '@material-ui/core/Grid'
import Test from '../../../models/Test'
import {
  Chip,
  Collapse,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Typography,
  WithStyles,
  withStyles
} from '@material-ui/core'
import HandleTestStyles from './HandleTestStyles'
import {VTextField} from '../../core'
import {inject} from '../../../infrastructure/di/inject'
import TotalTimeInput from './TotalTimeInput'
import Theme from '../../../models/Theme'
import {SelectProps} from '@material-ui/core/Select'
import {TestType} from '../../../common/enums'
import Discipline from '../../../models/Discipline'
import DisciplineTable from '../../Table/DisciplineTable'
import Block, {PBlock} from '../../Blocks/Block'
import {RouteComponentProps} from 'react-router'
import ITestService from '../../../services/TestService'
import IThemeService from '../../../services/ThemeService'
import INotificationService from '../../../services/NotificationService'
import IDisciplineService from '../../../services/DisciplineService'
import {Breadcrumbs} from '@material-ui/lab'
import classNames from 'classnames'
import {MtBlock} from '../../stuff/Margin'
import {IsMobileAsFuncChild} from '../../stuff/OnMobile'
import Input from '../../stuff/Input'
import Button from '../../stuff/Button'

type RouteParam = { id: string }
type TProps = WithStyles<typeof HandleTestStyles> & RouteComponentProps<RouteParam>

interface IState {
  Model: Test,
  AvailableThemes: Array<Theme>,
  SelectedThemes: Array<number>,
  ShowDisciplinesTable: boolean
}

let initState = {
  Model: new Test(),
  AvailableThemes: [],
  SelectedThemes: [],
  ShowDisciplinesTable: true
} as IState

class HandleTest extends Component<TProps, IState> {
  @inject private TestService?: ITestService
  @inject private ThemeService?: IThemeService
  @inject private DisciplineService?: IDisciplineService
  @inject private NotificationService?: INotificationService

  constructor(props: TProps) {
    super(props)

    this.state = initState
    this.state.Model.TotalTime = 60
  }

  async componentDidMount() {
    if (this.props.match.params.id) {
      const {data, success} = await this.TestService!.get(Number(this.props.match.params.id))

      if (success && data && data.DisciplineId) {
        let disciplinePromise = this.DisciplineService!.get(data.DisciplineId!)
        let disciplineThemesPromise = this.ThemeService!.getByDisciplineId(data.DisciplineId!, {All: true})
        let testThemesPromise = this.ThemeService!.getByTestId(data.Id!, {All: true})

        const {data: discipline} = await disciplinePromise
        const {data: disciplineThemes} = await disciplineThemesPromise
        const {data: testThemes} = await testThemesPromise

        if (discipline && disciplineThemes) {
          const model: Test = {
            ...data,
            Themes: testThemes ? testThemes.Items : [],
            Discipline: discipline
          }
          this.setState({
            Model: model,
            ShowDisciplinesTable: false,
            AvailableThemes: disciplineThemes ? disciplineThemes.Items : [],
            SelectedThemes: testThemes ? testThemes.Items.map(t => t.Id!) : []
          })
        } else {
          this.setState({Model: data})
        }
      }
    }
  }

  handleModel = ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any) =>
    this.setState(state => ({Model: {...state.Model, [name]: value}}))

  handleSelect = ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any) => {
    this.setState(state => ({
      Model: {
        ...state.Model,
        [name]: this.state.AvailableThemes!.filter((theme: Theme) => (value as Array<number>).includes(theme.Id!))
      },
      SelectedThemes: [...value]
    }))
  }

  handleChangeDiscipline = async (discipline: Discipline) => {
    if (discipline.Id !== this.state.Model.DisciplineId) {
      const {data, success} = await this.ThemeService!.getByDisciplineId(discipline.Id!, {All: true})

      if (success && data) {
        this.setState(state => ({
          Model: {
            ...state.Model,
            DisciplineId: discipline.Id,
            Discipline: discipline,
            Themes: []
          },
          AvailableThemes: data.Items,
          SelectedThemes: [],
          ShowDisciplinesTable: false
        }))
      }
    } else {
      this.setState({ShowDisciplinesTable: false})
    }
  }

  handleSubmit = async () => {
    if (this.state.Model.Id) {
      if (await this.TestService!.update(this.state.Model)) {
        this.NotificationService!.showSuccess(`Тест '${this.state.Model.Subject}' успешно обновлен`)
      }
    } else {
      const {data, success} = await this.TestService!.add(this.state.Model)

      if (success && data) {
        this.NotificationService!.showSuccess(`Тест '${this.state.Model.Subject}' успешно добавлен`)
        this.setState(initState)
      }
    }
  }

  renderSelectValues = (ids: SelectProps['value']): React.ReactNode => {
    let themes = this.state.AvailableThemes!
      .filter((theme: Theme) => (ids as Array<number>).includes(theme.Id as number))

    return <>
      {themes.map((theme: Theme) => <Chip key={theme.Id} label={theme.Name}/>)}
    </>
  }

  renderAttempts = (from: number, to: number): Array<React.ReactNode> => {
    let attempts = []
    do {
      attempts.push(<MenuItem key={from} value={from}>{from}</MenuItem>)
    } while (from++ < to)
    return attempts
  }

  render() {
    let {classes} = this.props

    const BreadcrumbsHeader = () =>
      <Grid item xs={12} className={classes.header} container zeroMinWidth wrap='nowrap' alignItems='center'>
        <Breadcrumbs separator={
          <Typography variant='subtitle1' className={classes.headerText}>
            >
          </Typography>
        }>
          <Typography align='center' noWrap variant='subtitle1'
                      className={classNames(classes.headerText, classes.breadcrumbs, {
                        [classes.breadcrumbsClickable]: !!this.state.Model.DisciplineId
                      })}
                      onClick={() => this.setState({ShowDisciplinesTable: true})}
          >
            {this.state.Model.DisciplineId ? this.state.Model.Discipline!.Name : 'Дисциплины'}
          </Typography>
          {
            this.state.Model.DisciplineId && !this.state.ShowDisciplinesTable &&
            <Typography align='center' noWrap variant='subtitle1'
                        className={classNames(classes.headerText, classes.breadcrumbs)}>
              Тест
            </Typography>
          }
        </Breadcrumbs>
      </Grid>

    return <Grid container justify='center'>
      <Grid item xs={12}>
        <IsMobileAsFuncChild>
          {(isMobile: boolean) =>
            <Block partial={!isMobile} empty={isMobile} topBot={isMobile}>
              <BreadcrumbsHeader/>
              <MtBlock value={4}/>
              <Grid item xs={12}>
                <Collapse timeout={500} in={this.state.ShowDisciplinesTable}>
                  <Grid container>
                    <DisciplineTable handleClick={this.handleChangeDiscipline}/>
                  </Grid>
                </Collapse>
              </Grid>
              <Grid item xs={12}>
                <Collapse timeout={500} in={!this.state.ShowDisciplinesTable}>
                  <Grid container>
                    <PBlock left={isMobile}>
                      <Grid item xs={12} container spacing={8} className={classes.inputsBlock}>
                        <Grid item xs={12} container direction='column'>
                          <VTextField
                            id='Subject'
                            name='Subject'
                            label='Название'
                            value={this.state.Model.Subject}
                            onChange={this.handleModel}
                            required
                            fullWidth
                            validators={{
                              max: {value: 255, message: 'Название не должно привышать 255 символов'},
                              required: true
                            }}
                          />
                        </Grid>
                        <Grid item xs={12} md={6} lg={3} style={{}}>
                          <TotalTimeInput
                            id='TotalTime'
                            name='TotalTime'
                            label='Длительность'
                            value={this.state.Model.TotalTime}
                            onChange={this.handleModel}
                            required
                            type='duration'
                            validators={{
                              max: {value: 3600, message: 'Тест не может быть больше 60 минут'}
                            }}
                            mask={[/[0-9]/, /[0-9]/, ':', /[0-5]/, /[0-9]/]}
                          />
                        </Grid>
                        <Grid item xs={12} md={6} lg={3}>
                          <FormControl fullWidth margin='normal'>
                            <InputLabel htmlFor='Attempts'>Попытки</InputLabel>
                            <Select name='Attempts'
                                    value={this.state.Model.Attempts}
                                    onChange={this.handleModel}
                                    input={<Input id='Attempts'/>}
                            >
                              {this.renderAttempts(1, 10)}
                            </Select>
                          </FormControl>
                        </Grid>
                        <Grid item xs={12} md={6} lg={3}>
                          <FormControl fullWidth margin='normal'>
                            <InputLabel htmlFor='Type'>Тип</InputLabel>
                            <Select
                              name='Type'
                              value={this.state.Model.Type}
                              onChange={this.handleModel}
                              input={
                                <Input id='Type'/>
                              }
                            >
                              <MenuItem value={TestType.Control}>
                                Контрольный
                              </MenuItem>
                              <MenuItem value={TestType.Teaching}>
                                Обучающий
                              </MenuItem>
                            </Select>
                          </FormControl>
                        </Grid>
                        <Grid item xs={12} md={6} lg={3}>
                          <FormControl fullWidth margin='normal'>
                            <InputLabel htmlFor='IsActive'>Активный</InputLabel>
                            <Select
                              name='IsActive'
                              value={Number(this.state.Model.IsActive)}
                              onChange={(e: any) => {
                                e.target.value = !!e.target.value
                                this.handleModel(e)
                              }}
                              input={
                                <Input id='IsActive'/>
                              }
                            >
                              <MenuItem value={0}>
                                Нет
                              </MenuItem>
                              <MenuItem value={1}>
                                Да
                              </MenuItem>
                            </Select>
                          </FormControl>
                        </Grid>
                      </Grid>
                      <MtBlock/>
                      <Grid item xs={12} container spacing={8} className={classes.inputsBlock}>
                        <Grid item xs={12}>
                          <FormControl fullWidth>
                            <InputLabel shrink>Темы</InputLabel>
                            <Select
                              multiple
                              name='Themes'
                              value={this.state.SelectedThemes}
                              onChange={this.handleSelect}
                              input={
                                <Input id='select-multiple-chip'/>
                              }
                              renderValue={this.renderSelectValues}
                              classes={{
                                selectMenu: classes.selectMenu
                              }}
                            >
                              {this.state.AvailableThemes.map((theme: Theme) =>
                                <MenuItem key={theme.Id} value={theme.Id}>
                                  {theme.Name}
                                </MenuItem>
                              )}
                            </Select>
                          </FormControl>
                        </Grid>
                      </Grid>
                      <Grid container className={classes.buttonBlock}>
                        <Grid item>
                          <Button mainColor='blue' onClick={this.handleSubmit}>
                            {this.state.Model.Id ? 'Обновить' : 'Добавить'}
                          </Button>
                        </Grid>
                      </Grid>
                    </PBlock>
                  </Grid>
                </Collapse>
              </Grid>
            </Block>
          }
        </IsMobileAsFuncChild>
      </Grid>
    </Grid>
  }
}

export default withStyles(HandleTestStyles)(HandleTest) as any