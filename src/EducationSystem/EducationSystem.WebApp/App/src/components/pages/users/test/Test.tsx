import {Grid, Typography, WithStyles, withStyles} from '@material-ui/core'
import {TNotifierProps, withNotifier} from '../../../../providers/NotificationProvider'
import {TestStyles} from './TestStyles'
import * as React from 'react'
import {Component} from 'react'
import Block from '../../../Blocks/Block'
import {RouteComponentProps} from 'react-router'
import {inject} from '../../../../infrastructure/di/inject'
import IStudentService from '../../../../services/abstractions/IStudentService'
import TestExecution from '../../../../models/TestExecution'
import {handleResult} from '../../../../helpers/Exception'

type TProps = WithStyles<typeof TestStyles> & TNotifierProps & RouteComponentProps<{ id: string }>

interface IState {
  Model?: TestExecution
}

class Test extends Component<TProps, IState> {
  @inject private StudentService?: IStudentService
  
  constructor(props: TProps) {
    super(props)

    this.state = {}
  }

  async componentDidMount() {
    handleResult(
      await this.StudentService!.getTestExecution(Number(this.props.match.params.id)),
      this.props.notifier.error,
      ((data: TestExecution) => this.setState({
        Model: data
      }))
    )
  }

  render(): React.ReactNode {
    let {classes} = this.props

    return <Grid container justify='center' spacing={40}>
      <Grid item xs={12} md={10} lg={8}>
        <Block partial>
          <Grid item xs={12} container zeroMinWidth wrap='nowrap' justify='center'>
            <Typography align='center' noWrap color='inherit'>
              Не найдено
            </Typography>
          </Grid>
        </Block>
      </Grid>
    </Grid>
  }
}

export default withStyles(TestStyles)(
  withNotifier(
    Test)
) as any