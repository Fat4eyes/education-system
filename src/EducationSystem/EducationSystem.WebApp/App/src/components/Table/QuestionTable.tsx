import {Grid, Typography} from '@material-ui/core'
import {IPagingOptions} from '../../models/PagedData'
import * as React from 'react'
import {Component, ComponentType} from 'react'
import {inject} from '../../infrastructure/di/inject'
import Question from '../../models/Question'
import {SortableArrayContainer, SortableArrayItem} from '../stuff/SortableArray'
import {arrayMove, SortEnd} from 'react-sortable-hoc'
import {TNotifierProps, withNotifier} from '../../providers/NotificationProvider'
import IQuestionService from '../../services/QuestionService'
import {withAuthenticated} from '../../providers/AuthProvider/AuthProvider'
import {TAuthProps} from '../../providers/AuthProvider/AuthProviderTypes'
import RowHeader from './RowHeader'
import ClearIcon from '@material-ui/icons/Clear'
import Modal from '../stuff/Modal'

interface IProps {
  themeId: number,
  loadCallback: () => void,
  handleClick: (id: number) => void
}

type TProps = IProps & TNotifierProps & TAuthProps

interface IState {
  Items: Array<Question>
  DeleteQuestionId?: number
}

class QuestionTable extends Component<TProps, IState> {
  @inject
  private QuestionService?: IQuestionService

  constructor(props: TProps) {
    super(props)

    this.state = {
      Items: []
    }
  }

  getTableData = async (param: IPagingOptions = {All: true}) => {
    const {data, success} = await this.QuestionService!.getByThemeId(this.props.themeId, param)

    if (success && data) {
      data.Items.sort((a, b) => a.Order !== undefined && b.Order !== undefined && a.Order > b.Order ? 1 : -1)
      this.setState({Items: data.Items}, this.props.loadCallback)
    }
  }

  componentDidMount() {this.getTableData()}

  handleSort = ({oldIndex, newIndex}: SortEnd) => {
    if (oldIndex === newIndex) return

    const questions = arrayMove(this.state.Items, oldIndex, newIndex)
      .map((question, index): Question => ({...question, Order: index}))

    this.setState(
      {Items: questions},
      async () => {
        await this.QuestionService!.setOrder(this.props.themeId, questions)
      }
    )
  }

  handleModal = (id?: number) => () => {
    this.setState({
      DeleteQuestionId: id
    })
  }

  handleDelete = async () => {
    const id = this.state.DeleteQuestionId

    this.handleModal()()

    if (id && await this.QuestionService!.delete(id)) {
      this.setState(state => ({
        Items: state.Items.filter((question: Question) => question.Id !== id)
      }))
    }
  }

  render() {
    const {auth: {User}} = this.props
    const isLecturer = User && User.Roles && User.Roles.Lecturer

    return <Grid item xs={12} container justify='center'>
      {
        isLecturer &&
        <SortableArrayContainer onSortEnd={this.handleSort} useDragHandle>
          {this.state.Items.map((question: Question, index: number) =>
            <SortableArrayItem key={question.Id} index={index} value={
              <>
                <Grid item xs container zeroMinWidth
                      onClick={() => this.props.handleClick(question.Id!)}>
                  <Typography variant='subtitle1'>
                    {question.Text}
                  </Typography>
                </Grid>
                <ClearIcon color='action' onClick={this.handleModal(question.Id)}/>
              </>
            }
            />
          )}
        </SortableArrayContainer>
      }
      {
        !isLecturer &&
        this.state.Items.map((question: Question) =>
          <Grid item xs={12} container key={question.Id}>
            <RowHeader>
              <Grid item xs container zeroMinWidth alignItems='center'>
                <Typography variant='subtitle1'>
                  {question.Text}
                </Typography>
              </Grid>
              <ClearIcon color='action' onClick={this.handleModal(question.Id)}/>
            </RowHeader>
          </Grid>
        )}
      }
      <Modal
        isOpen={!!this.state.DeleteQuestionId}
        onClose={this.handleModal()}
        onYes={this.handleDelete}
        onNo={this.handleModal()}
      />
    </Grid>
  }
}

export default withAuthenticated(withNotifier(QuestionTable)) as ComponentType<IProps>