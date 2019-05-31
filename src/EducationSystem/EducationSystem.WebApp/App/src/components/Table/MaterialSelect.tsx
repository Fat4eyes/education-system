import TableComponent from './TableComponent'
import Material, {IMaterialAnchor} from '../../models/Material'
import {ITableState} from './IHandleTable'
import {IPagingOptions} from '../../models/PagedData'
import IMaterialService from '../../services/MaterialService'
import {inject} from '../../infrastructure/di/inject'
import {TNotifierProps, withNotifier} from '../../providers/NotificationProvider'
import * as React from 'react'
import {ChangeEvent} from 'react'
import {
  Chip,
  createStyles,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  Theme,
  Typography,
  WithStyles,
  withStyles
} from '@material-ui/core'
import RowHeader from './RowHeader'
import {TablePagination} from '../core'
import {SelectProps} from '@material-ui/core/Select'
import Input from '../stuff/Input'
import {MtBlock} from '../stuff/Margin'

const styles = (theme: Theme) => createStyles({
  chipLabel: {
    maxWidth: '50vw',
    margin: 'auto'
  },
  chip: {
    width: '100%'
  },
  selectMenu: {
    display: 'flex',
    flexWrap: 'wrap',
    '& div': {
      padding: theme.spacing.unit,
      margin: theme.spacing.unit
    }
  }
})

interface IProps {
  selectedMaterial?: Material
  onSelectMaterial: (material?: Material) => void
  selectedAnchors: Array<IMaterialAnchor>
  handleSelectAnchors: ({target: {name, value}}: ChangeEvent<HTMLInputElement> | any) => void
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
    let {data, success} = await this.MaterialService!.getAll({
      ...this.firstPageOptions,
      ...param
    }, this.getNameFilter(this.state.Name))

    if (success && data) {
      this.setState({
        Count: data.Count,
        Items: data.Items,
        IsLoading: false
      })
    }
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

  renderSelectValues = (ids: SelectProps['value']): React.ReactNode => {
    if (!this.props.selectedMaterial) return

    const anchors = this.props.selectedMaterial
      .Anchors.filter((anchor: IMaterialAnchor) => (ids as Array<number>).includes(anchor.Id as number))

    return <>
      {anchors.map((anchor: IMaterialAnchor) => <Chip key={anchor.Id} label={anchor.Name}/>)}
    </>
  }


  render(): React.ReactNode {
    const {selectedMaterial, onSelectMaterial, classes} = this.props
    // @ts-ignore
    return <Grid container justify='center'>
      <Grid item xs={12}>
        {
          selectedMaterial &&
          <Grid container>
            <MtBlock value={2}/>
            <Chip
              classes={{label: classes.chipLabel}}
              className={classes.chip}
              label={<Grid item xs container wrap='nowrap' zeroMinWidth>
                <Typography noWrap variant='subtitle1'>
                  {selectedMaterial.Name}
                </Typography>
              </Grid>}
              onDelete={() => onSelectMaterial()}
              variant='outlined'
            />
            {
              selectedMaterial.Anchors && selectedMaterial.Anchors.length && <>
                <MtBlock value={2}/>
                <Grid item xs={12}>
                  <FormControl fullWidth>
                    <InputLabel htmlFor='select-multiple-chip'>Якоря:</InputLabel>
                    <Select
                      multiple
                      name='Anchors'
                      value={this.props.selectedAnchors.map(a => a.Id)}
                      onChange={this.props.handleSelectAnchors}
                      input={
                        <Input id='select-multiple-chip'/>
                      }
                      renderValue={this.renderSelectValues}
                      classes={{
                        selectMenu: classes.selectMenu
                      }}
                    >
                      {selectedMaterial.Anchors.map((anchor: IMaterialAnchor) =>
                        <MenuItem key={anchor.Id} value={anchor.Id}>
                          {anchor.Name}
                        </MenuItem>
                      )}
                    </Select>
                  </FormControl>
                </Grid>
              </>
            }
          </Grid>
        }
        {
          !selectedMaterial &&
          <Grid container>
            <Grid item xs={12}>
              <FormControl fullWidth>
                <InputLabel shrink>
                  Название материала:
                </InputLabel>
                <Input
                  value={this.state.Name}
                  onChange={this.handleName}
                  fullWidth
                />
              </FormControl>
            </Grid>
            <MtBlock value={2}/>
            {
              this.state.Count > this.state.Items.length &&
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
            }
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