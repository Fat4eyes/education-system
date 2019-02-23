import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {withSnackbar} from 'notistack'
import {Paper, StepContent, Stepper, Typography, withStyles} from '@material-ui/core'
import HandleTestStyles from './HandleTestStyles'
import Grid from '@material-ui/core/Grid'
import {Snackbar} from './../../../helpers'
import EditIcon from '@material-ui/icons/EditRounded'
import StepLabel from '@material-ui/core/StepLabel'
import TextField from '@material-ui/core/TextField'
import Step from '@material-ui/core/Step'
import VTextField from '../../stuff/VTextField'
import VMaskedField from '../../stuff/VMaskedField'

@withSnackbar
@withStyles(HandleTestStyles)
class HandleTest extends Component {
  constructor(props) {
    super(props)

    this.state = {
      model: {
        Name: '',
        Duration: '10:00',
        Attempts: 0,
        Type: 0,
        Themes: [],
        Materials: [],
        IsActive: false
      },
      activeStep: 0,
      stepsCount: 2
    }

    this.Snackbar = new Snackbar(this.props.enqueueSnackbar)
  }

  handleStep = previous => step => {
    const normalize = number => {
      if (number < 0)
        return 0
      if (number > this.state.stepsCount)
        return this.state.stepsCount
      return number
    }
    
    return this.setState(state => ({
      activeStep: step !== undefined 
        ? step
        : normalize(state.activeStep + (previous ? -1 : 1))
    }))
  }

  handleModel = ({target: {name, value}}) =>
    this.setState(state => ({
      model: {
        ...state.model,
        [name]: value
      }
    }))

  handleKeyDown = ({key}) => {
    if (key === 'Enter') {
      this.handleStep()()
    }
  }

  render() {
    const {classes, edit} = this.props
    const {model} = this.state

    const Header = () => (
      <Grid container item xs={12} alignItems='center' spacing={16}>
        <Grid item>
          <EditIcon color='primary' fontSize='large'/>
        </Grid>
        <Grid item xs container wrap='nowrap' zeroMinWidth>
          <Typography noWrap variant='subtitle1'>
            {edit ? 'Редактирование теста' : 'Создание теста'}
          </Typography>
        </Grid>
      </Grid>
    )

    return <Grid container justify='center'>
      <Grid item xs={12} md={10} lg={8}>
        <Paper className={classes.paper}>
          <Header/>
          <Stepper activeStep={this.state.activeStep} orientation='vertical' className={classes.stepper}>
            <Step>
              <StepLabel onClick={() => this.handleStep()(0)}>Основная информация</StepLabel>
              <StepContent>
                <Grid item xs={12} container  spacing={16}>
                  <Grid item xs={12} container direction='column'>
                    <VTextField
                      id='Name' name='Name' label='Название'
                      value={model.Name} onChange={this.handleModel}
                      margin='normal' required fullWidth
                      validators={{
                        max: {value: 255, message: 'Название не должно привышать 255 символов'},
                        required: true
                      }}
                      className={classes.input}
                      onKeyDown={this.handleKeyDown}
                    />
                  </Grid>
                  <Grid item xs={12} md={6} lg={4}>
                    <VMaskedField
                      id='Duration' name='Duration' label='Длительность теста'
                      value={model.Duration} onChange={this.handleModel}
                      required
                      type='duration'
                      validators={{
                        max: {value: 3600, message: 'Тест не может быть больше 60 минут'}
                      }}
                      className={classes.input}
                      onKeyDown={this.handleKeyDown}
                      mask={[/[0-9]/, /[0-9]/, ':', /[0-5]/, /[0-9]/]}
                      styles={{
                        input: {
                          width: 41,
                          marginLeft: 8
                        }
                      }}
                    />
                  </Grid>
                  <Grid item xs={6}>

                  </Grid>
                </Grid>
              </StepContent>
            </Step>
            <Step>
              <StepLabel onClick={() => this.handleStep()(1)}>Основная информация</StepLabel>
              <StepContent>
                <Grid container item xs={12} spacing={16}>
                  <VTextField
                    id='Name' name='Name' label='Название'
                    value={model.Name} onChange={this.handleModel}
                    margin='normal' required fullWidth
                    validators={{
                      max: {value: 255, message: 'Название не должно привышать 255 символов'},
                      required: true
                    }}
                    className={classes.input}
                    onKeyDown={this.handleKeyDown}
                  />
                </Grid>
              </StepContent>
            </Step> 
          </Stepper>
        </Paper>
      </Grid>
    </Grid>
  }
}

HandleTest.defaultProps = {
  edit: false
}

HandleTest.propTypes = {
  edit: PropTypes.bool
}

export default HandleTest