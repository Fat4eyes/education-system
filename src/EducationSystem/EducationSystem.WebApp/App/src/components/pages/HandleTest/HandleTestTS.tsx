import * as React from 'react'
import {ChangeEvent, Component, KeyboardEvent} from 'react'
import Grid from '@material-ui/core/Grid'
import Test from '../../../models/Test'
import {Paper, Typography, WithStyles, withStyles} from '@material-ui/core'
import HandleTestStyles from './HandleTestStyles'
import {InjectedNotistackProps, withSnackbar} from 'notistack'
import EditIcon from '@material-ui/icons/Edit'
import {VTextField} from '../../core'
import VMaskedField from '../../stuff/VMaskedField'
import ITestService from '../../../services/abstractions/ITestService'
import {inject} from '../../../infrastructure/di/inject'

interface IProps extends WithStyles<typeof HandleTestStyles>, InjectedNotistackProps {
  isEdit: boolean
}

interface IState {
  Model: Test
}

class HandleTest extends Component<IProps, IState> {
  @inject
  private TestService?: ITestService
  
  constructor(props: IProps) {
    super(props)
    this.state = {
      Model: new Test()
    }
  }

  handleModel = ({target: {name, value}}: ChangeEvent<HTMLInputElement>) =>
    this.setState(state => ({
      Model: {
        ...state.Model,
        [name]: value
      }
    }), () => console.log(this.state.Model))
  
  handleTotalTime = (get: boolean) => (value?: string) => {
    
  } 

  render() {
    
    console.log(this.TestService!.test())
    
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
              <VMaskedField
                id='TotalTime' name='TotalTime' label='Длительность теста'
                value={this.state.Model.TotalTime} onChange={this.handleModel}
                required
                type='duration'
                validators={{
                  max: {value: 3600, message: 'Тест не может быть больше 60 минут'}
                }}
                mask={[/[0-9]/, /[0-9]/, ':', /[0-5]/, /[0-9]/]}
                styles={{}}
              />
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
              <VTextField
                id='Attempts' name='Attempts' label='Колличество попыток'
                value={this.state.Model.Attempts} onChange={this.handleModel}
                margin='normal' required fullWidth
                validators={{
                  min: {value: 1, message: ''},
                  required: true
                }}
              />
            </Grid>
          </Grid>
        </Paper>
      </Grid>
    </Grid>
  }
}

export default withSnackbar(withStyles(HandleTestStyles)(HandleTest) as any)