import TableComponent from './TableComponent'
import Material from '../../models/Material'
import {ITableState} from './IHandleTable'
import IPagedData, {IPagingOptions} from '../../models/PagedData'
import IMaterialService from '../../services/abstractions/IMaterialService'
import {inject} from '../../infrastructure/di/inject'
import {TNotifierProps, withNotifier} from '../../providers/NotificationProvider'
import * as React from 'react'
import {ChangeEvent} from 'react'
import {Exception} from '../../helpers'
import {Chip, createStyles, Grid, TextField, Theme, Typography, WithStyles, withStyles} from '@material-ui/core'
import RowHeader from './RowHeader'
import {TablePagination} from '../core'

const styles = (theme: Theme) => createStyles({
  chipLabel: {
    maxWidth: '50vw'
  }
})

interface IProps {
  selectedMaterial?: Material
  onSelectMaterial: (material?: Material) => void
}

type TProps = TNotifierProps & IProps & WithStyles<typeof styles>

interface IState extends ITableState<Material> {
  Name: string
}

class MaterialSelect extends TableComponent<Material, TProps, IState> {
  @inject private readonly MaterialService?: IMaterialService
  private readonly firstPageOptions: IPagingOptions

  constructor(props: TProps) {
    super(props)

    this.state = {
      Count: 0,
      CountPerPage: 10,
      Page: 0,
      Items: [],
      IsLoading: false,
      Name: ''
    }

    this.firstPageOptions = {
      Skip: 0,
      Take: this.state.CountPerPage
    }
  }

  componentDidMount() {
    this.setState({IsLoading: true}, () => this.getTableData(this.firstPageOptions))
  }

  async getTableData(param: IPagingOptions): Promise<any> {
    let result = await this.MaterialService!.getAll({
      ...this.firstPageOptions,
      ...param
    }, this.getNameFilter(this.state.Name))
    if (result instanceof Exception) {
      return this.props.notifier.error(result.message)
    }

    this.setState({
      Count: (result as IPagedData<Material>).Count,
      Items: (result as IPagedData<Material>).Items,
      IsLoading: false
    })
  }

  handleName = ({target: {value}}: ChangeEvent<HTMLInputElement> | any) => {
    if (this.isNeedToReloadData(value, this.state.Name))
      this.setState({Name: value, IsLoading: true, Page: 0}, () => this.getTableData(this.firstPageOptions))
    else
      this.setState({Name: value})
  }

  isSelected = (material: Material) => {
    let {selectedMaterial} = this.props
    return selectedMaterial && selectedMaterial.Id === material.Id
  }

  render(): React.ReactNode {
    const {selectedMaterial, onSelectMaterial, classes} = this.props
    return <Grid container justify='center' spacing={16}>
      <Grid item xs={12}>
        {
          selectedMaterial &&
          <Chip
            classes={{label:classes.chipLabel}}
            label={<Grid container wrap='nowrap' zeroMinWidth>
              <Typography noWrap variant='subtitle1'>
                {selectedMaterial.Name}
              </Typography>
            </Grid>}
            onDelete={() => onSelectMaterial()}
            variant='outlined'
          />
        }
        {
          !selectedMaterial &&
          <Grid container>
            <Grid item xs={12}>
              <TextField
                label='Название материала'
                placeholder='Название материала (больше 3 символов)'
                value={this.state.Name}
                onChange={this.handleName}
                style={{marginBottom: 4}}
                fullWidth
                margin='none'
              />
            </Grid>
            <Grid item xs={12}>
              <TablePagination
                page={this.state.Page}
                count={{
                  all: this.state.Count,
                  perPage: this.state.CountPerPage,
                  current: this.state.Items.length
                }}
                onPageChange={this.handleChangePage}
                onCountPerPageChange={this.handleChangeRowsPerPage}
                showChangeCountPerPageBlock
              />
            </Grid>
            <Grid item xs={12}>
              {this.state.Items.map((material: Material) =>
                <RowHeader
                  key={material.Id}
                  onClick={() => this.props.onSelectMaterial(material)}
                  selected={this.isSelected(material)}
                >
                  <Grid item xs={12} container wrap='nowrap' zeroMinWidth>
                    <Typography noWrap variant='subtitle1'>
                      {material.Name}
                    </Typography>
                  </Grid>
                </RowHeader>
              )}
            </Grid>
          </Grid>
        }
      </Grid>
    </Grid>
  }

}

export default withStyles(styles)(withNotifier(MaterialSelect)) as any