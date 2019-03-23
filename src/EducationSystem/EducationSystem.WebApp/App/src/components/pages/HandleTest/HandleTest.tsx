import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import Grid from '@material-ui/core/Grid'
import Test from '../../../models/Test'
import {
  Button,
  Chip,
  FormControl, FormControlLabel,
  Input,
  InputLabel,
  MenuItem,
  Paper,
  Select,
  Step,
  StepContent,
  StepLabel,
  Stepper, Switch,
  Typography,
  WithStyles,
  withStyles
} from '@material-ui/core'
import HandleTestStyles from './HandleTestStyles'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import EditIcon from '@material-ui/icons/Edit'
import {If, VTextField} from '../../core'
import ITestService from '../../../services/abstractions/ITestService'
import {inject} from '../../../infrastructure/di/inject'
import TotalTimeInput from './TotalTimeInput'
import Theme from '../../../models/Theme'
import {SelectProps} from '@material-ui/core/Select'
import IDisciplineService from '../../../services/abstractions/IDisciplineService'
import {TestType} from '../../../common/enums'
import {Exception} from '../../../helpers'
import IPagedData from '../../../models/PagedData'
import Discipline from '../../../models/Discipline'
import DisciplineTable from '../../Table/DisciplineTable'

interface IProps extends WithStyles<typeof HandleTestStyles>, InjectedNotistackProps {
  isEdit: boolean,
  match: {
    params: {
      disciplineId: number
    }
  }
}

interface IState {
  Model: Test,
  AvailableThemes: Array<Theme>,
  SelectedThemes: Array<number>,
  step: number
}

const initState = {
  Model: new Test(),
  AvailableThemes: [],
  SelectedThemes: [],
  step: 0
} as IState

class HandleTest extends Component<IProps, IState> {
  @inject
  private TestService?: ITestService

  @inject
  private DisciplineService?: IDisciplineService

  constructor(props: IProps) {
    super(props)

    this.state = initState
    this.state.Model.TotalTime = 60
  }
  
  handleModel = ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any) => {
    this.setState(state => ({
      Model: {
        ...state.Model,
        [name]: value
      }
    }))
  }

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
      let result = await this.DisciplineService!.getThemes(discipline.Id!)

      if (result instanceof Exception) {
        return this.props.enqueueSnackbar(result.message, {
          variant: 'error',
          anchorOrigin: {
            vertical: 'bottom',
            horizontal: 'right'
          }
        })
      }

      this.setState(state => ({
        Model: {
          ...state.Model,
          DisciplineId: discipline.Id,
          Discipline: discipline
        },
        AvailableThemes: (result as IPagedData<Theme>).Items,
        SelectedThemes: [],
        step: state.step + 1
      }))
    } else {
      this.setState(state => ({
        step: state.step + 1
      }))
    }
  }

  handleSubmit = async () => {
    let result = await this.TestService!.add(this.state.Model)

    if (result instanceof Exception) {
      return this.props.enqueueSnackbar(result.message, {
        variant: 'error',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })
    }
    
    if ((result as Test).Id) {
      this.props.enqueueSnackbar(`Тест "${result.Subject}" успешно добавлен`, {
        variant: 'success',
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'right'
        }
      })
      
      this.setState(initState)
    }
  }

  handleSteps = {
    next: () => this.setState(state => ({step: state.step + 1})),
    back: () => this.setState(state => ({step: state.step - 1})),
    reset: () => this.setState({step: 0})
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
    let {classes, isEdit} = this.props
    const Header = () => (
      <Grid container item xs={12} alignItems='center' spacing={16}>
        <Grid item>
          <EditIcon color='primary' fontSize='large'/>
        </Grid>
        <Grid item xs container wrap='nowrap' zeroMinWidth>
          <Typography noWrap variant='subtitle1'>
            {isEdit ? 'Редактирование теста' : 'Создание теста'}
          </Typography>
        </Grid>
      </Grid>
    )
    return <Grid container justify='center'>
      <Grid item xs={12} md={10} lg={8}>
        <Paper className={classes.paper}>
          <Header/>
          <Stepper activeStep={this.state.step} orientation='vertical' className={classes.stepper}>
            <Step>
              <StepLabel>
                <Typography noWrap variant='subtitle1'>
                  {!!this.state.Model.DisciplineId 
                    ? `Дисциплина: ${this.state.Model.Discipline!.Name}`
                    : 'Выбор дисциплины'
                  }
                </Typography>
              </StepLabel>
              <StepContent>
                <Grid container>
                  <DisciplineTable handleClick={this.handleChangeDiscipline}/>
                </Grid>
              </StepContent>
            </Step>
            <Step>
              <StepLabel>
                <Typography noWrap variant='subtitle1'>
                  Создание теста
                </Typography>
              </StepLabel>
              <StepContent>
                <Grid container>
                  <Grid item xs={12} container spacing={16}>
                    <Grid item xs={12} container direction='column'>
                      <VTextField
                        id='Subject' name='Subject' label='Название'
                        value={this.state.Model.Subject}
                        onChange={this.handleModel}
                        margin='normal' required fullWidth
                        validators={{
                          max: {value: 255, message: 'Название не должно привышать 255 символов'},
                          required: true
                        }}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6} md={4} lg={3}>
                      <TotalTimeInput
                        id='TotalTime' name='TotalTime' label='Длительность теста'
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
                    <Grid item xs={12} sm={6} md={4} lg={3}>
                      <FormControl fullWidth margin='normal'>
                        <InputLabel htmlFor='Attempts'>Количество попыток</InputLabel>
                        <Select name='Attempts'
                                value={this.state.Model.Attempts}
                                onChange={this.handleModel}
                                input={<Input id="Attempts"/>}
                        >
                          {this.renderAttempts(1, 10)}
                        </Select>
                      </FormControl>
                    </Grid>
                    <Grid item xs={12} sm={6} md={4} lg={3}>
                      <FormControl fullWidth margin='normal'>
                        <InputLabel htmlFor='Type'>Тип</InputLabel>
                        <Select
                          name='Type'
                          value={this.state.Model.Type}
                          onChange={this.handleModel}
                          input={
                            <Input id="Type"/>
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
                    <Grid item xs={12} sm={6} md={4} lg={3}>
                      <FormControl fullWidth margin='normal'>
                        <InputLabel htmlFor='IsActive'>Активный</InputLabel>
                        <Select
                          name='IsActive'
                          value={Number(this.state.Model.IsActive)}
                          onChange={(e: any) => { 
                            e.target.value = !!e.target.value;
                            this.handleModel(e)
                          }}
                          input={
                            <Input id="IsActive"/>
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
                    <Grid item xs={12}>
                      <FormControl fullWidth>
                        <InputLabel htmlFor="select-multiple-chip">Темы</InputLabel>
                        <Select
                          multiple
                          name='Themes'
                          value={this.state.SelectedThemes}
                          onChange={this.handleSelect}
                          input={
                            <Input id="select-multiple-chip"/>
                          }
                          renderValue={this.renderSelectValues}
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
                      <Button variant='outlined' onClick={this.handleSteps.back}>
                        К выбору дисциплины
                      </Button>
                    </Grid>
                    <Grid item>
                      <Button variant='contained' color='primary' onClick={this.handleSubmit}>
                        Добавить
                      </Button>
                    </Grid>
                  </Grid>
                </Grid>
              </StepContent>
            </Step>
          </Stepper>
        </Paper>
      </Grid>
    </Grid>
  }
}

export default withSnackbar(withStyles(HandleTestStyles)(HandleTest) as any)