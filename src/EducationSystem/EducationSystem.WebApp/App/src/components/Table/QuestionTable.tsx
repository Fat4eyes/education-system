import {Grid, Typography} from '@material-ui/core'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import * as React from 'react'
import {Component, ComponentType} from 'react'
import {inject} from '../../infrastructure/di/inject'
import Question from '../../models/Question'
import IThemeService from '../../services/abstractions/IThemeService'
import {SortableArrayContainer, SortableArrayItem} from '../stuff/SortableArray'
import {arrayMove, SortEnd} from 'react-sortable-hoc'
import {TNotifierProps, withNotifier} from '../../providers/NotificationProvider'
import {resultHandler} from '../../helpers/Exception'

interface IProps {
  themeId: number,
  loadCallback: () => void,
  handleClick: (id: number) => void
}

type TProps = IProps & TNotifierProps

interface IState {
  Items: Array<Question>
}

class QuestionTable extends Component<TProps, IState> {
  @inject
  private ThemeService?: IThemeService

  constructor(props: TProps) {
    super(props)

    this.state = {
      Items: []
    }
  }

  getTableData = async (param: IPagingOptions = {All: true}) => {
    resultHandler
      .onError(this.props.notifier.error)
      .onSuccess((data: IPagedData<Question>) => this.setState({Items: data.Items}, this.props.loadCallback))
      .handleResult(await this.ThemeService!.getQuestions(this.props.themeId, param))
  }

  componentDidMount() {this.getTableData()}

  handleSort = ({oldIndex, newIndex}: SortEnd) => {
    this.setState(({Items}) => ({Items: arrayMove(Items, oldIndex, newIndex)}))
  }

  render() {
    return <Grid item xs={12} container justify='center'>
      <SortableArrayContainer onSortEnd={this.handleSort} useDragHandle>
        {this.state.Items.map((question: Question, index: number) =>
          <SortableArrayItem key={question.Id} index={index} value={
            <Grid item xs={12} container zeroMinWidth 
                  onClick={() => this.props.handleClick(question.Id!)}>
              <Typography variant='subtitle1'>
                {question.Text}
              </Typography>
            </Grid>
          }
          />
        )}
      </SortableArrayContainer>
    </Grid>
  }
}

export default withNotifier(QuestionTable) as ComponentType<IProps>