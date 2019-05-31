import TableComponent from '../../Table/TableComponent'
import {FormControl, Grid, InputLabel, Typography, withStyles, WithStyles} from '@material-ui/core'
import MaterialStyles from './MaterialStyles'
import Material from '../../../models/Material'
import {getDefaultTableState, ITableState} from '../../Table/IHandleTable'
import {IPagingOptions} from '../../../models/PagedData'
import * as React from 'react'
import {ChangeEvent, ComponentType} from 'react'
import {inject} from '../../../infrastructure/di/inject'
import IMaterialService from '../../../services/MaterialService'
import Block, {PBlock} from '../../Blocks/Block'
import {IsMobileAsFuncChild} from '../../stuff/OnMobile'
import Input from '../../stuff/Input'
import {MrBlock, MtBlock} from '../../stuff/Margin'
import {TablePagination} from '../../core'
import AddButton from '../../stuff/AddButton'
import {routes} from '../../Layout/Routes'
import {withAuthenticated} from '../../../providers/AuthProvider/AuthProvider'
import {TAuthProps} from '../../../providers/AuthProvider/AuthProviderTypes'
import {Redirect} from 'react-router'
import RowHeader from '../../Table/RowHeader'
import ClearIcon from '@material-ui/icons/Clear'
import EditIcon from '@material-ui/icons/Edit'
import Modal from '../../stuff/Modal'
import BlockHeader from '../../Blocks/BlockHeader'

type TProps = WithStyles<typeof MaterialStyles> & TAuthProps

interface IState extends ITableState<Material> {
  Name: string
  RedirectTo?: string
  DeletableMaterialId?: number
}

class Materials extends TableComponent<Material, TProps, IState> {
  @inject private MaterialService!: IMaterialService

  constructor(props: TProps) {
    super(props)

    this.state = {
      ...getDefaultTableState(),
      Name: ''
    }
  }

  componentDidMount(): void {
    this.getTableData()
  }

  async getTableData(pagingOptions: IPagingOptions = {Skip: 0, Take: this.state.CountPerPage}, param: any = {}) {
    const {data, success} = await this.MaterialService!.getAll({
      ...this.getNameFilter(this.state.Name),
      ...pagingOptions
    })

    if (success && data) this.setState({...data, ...param})
  }

  private handleName = ({target: {value}}: ChangeEvent<HTMLInputElement> | any) => {
    const oldValue = this.state.Name
    if (this.isNeedToReloadData(value, oldValue))
      this.setState({Name: value}, () => this.getTableData({Skip: 0, Take: this.state.CountPerPage}))
    else
      this.setState({Name: value})
  }

  handleTest = {
    modal: (id?: number) => () => {
      this.setState({
        DeletableMaterialId: id
      })
    },
    delete: async () => {
      this.handleTest.modal()()

      if (this.state.DeletableMaterialId && await this.MaterialService!.delete(this.state.DeletableMaterialId!)) {
        let pagingOptions: IPagingOptions = {
          Skip: this.state.Page * this.state.CountPerPage,
          Take: this.state.CountPerPage
        }

        let page = this.state.Page

        if (this.state.Count === this.state.Page * this.state.CountPerPage + this.state.Items.length) {
          if (this.state.Page === 0) {
            pagingOptions.Skip = 0
            page = 0
          } else if (this.state.Items.length === 1) {
            pagingOptions.Skip = (this.state.Page - 1) * this.state.CountPerPage
          }
        }

        this.getTableData(pagingOptions, {Page: page})
      }
    }
  }

  render() {
    const {classes, auth: {User}} = this.props

    if (this.state.RedirectTo) return <Redirect to={this.state.RedirectTo}/>

    return <Grid container>
      <Grid item xs={12}>
        <IsMobileAsFuncChild>
          {(isMobile: boolean) =>
            <Block partial={!isMobile} empty={isMobile} topBot={isMobile}>
              <BlockHeader>
                <Typography noWrap variant='subtitle1' component='span'>
                  Материалы
                </Typography>
              </BlockHeader>
              <MtBlock value={3}/>
              <Grid item xs={12}>
                <PBlock left={isMobile}>
                  <FormControl fullWidth>
                    <InputLabel shrink htmlFor='Name'>
                      Название дисциплины:
                    </InputLabel>
                    <Input
                      value={this.state.Name}
                      name='Name'
                      onChange={this.handleName}
                      fullWidth
                    />
                  </FormControl>
                  <MtBlock value={4}/>
                  {
                    this.state.Count > this.state.Items.length && <>
                      <Grid item xs={12}>
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
                      </Grid>
                      <MtBlock value={isMobile ? 4 : 1}/>
                    </>
                  }
                </PBlock>
              </Grid>
              {
                User && User.Roles && User.Roles.Lecturer &&
                <Grid item xs={12}>
                  <AddButton onClick={() => this.setState({RedirectTo: routes.createMaterial})}/>
                </Grid>
              }
              {this.state.Items.map((material: Material, index: number) =>
                <Grid item xs={12} container key={material.Id || index}>
                  <Grid item xs={12}>
                    <RowHeader alignItems='center'>
                      <Grid item xs container wrap='nowrap' zeroMinWidth>
                        <Typography noWrap variant='subtitle1'>
                          {material.Name}
                        </Typography>
                      </Grid>
                      <Grid item xs={1}/>
                      {
                        User && User.Roles && User.Roles.Lecturer && <>
                          <EditIcon
                            fontSize='small'
                            color='action'
                            onClick={() => this.setState({RedirectTo: routes.editMaterial(material.Id!)})}
                          />
                          <MrBlock/>
                        </>
                      }
                      <ClearIcon color='action' onClick={this.handleTest.modal(material.Id)}/>
                    </RowHeader>
                  </Grid>
                </Grid>
              )}
            </Block>
          }
        </IsMobileAsFuncChild>
      </Grid>
      <Modal
        isOpen={!!this.state.DeletableMaterialId}
        onClose={this.handleTest.modal()}
        onYes={this.handleTest.delete}
        onNo={this.handleTest.modal()}
      />
    </Grid>
  }

}

export default withAuthenticated(withStyles(MaterialStyles)(Materials)) as ComponentType<{}>