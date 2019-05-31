import * as React from 'react'
import {FunctionComponent} from 'react'
import Discipline from '../../../../models/Discipline'
import {
  createStyles,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  Theme,
  withStyles,
  WithStyles
} from '@material-ui/core'
import Input from '../../../stuff/Input'
import {MrBlock, MtBlock} from '../../../stuff/Margin'

const styles = (theme: Theme) => createStyles({})

interface IProps {
  Disciplines: Array<Discipline>,
  DisciplineId: number,
  IsActive: boolean,
  handleInput: any,
  handleSearch: any,
  Name: string,
  IsMobile: boolean
}

type TProps = IProps & WithStyles<typeof styles>
const Filter: FunctionComponent<TProps> = ({classes, handleSearch, handleInput, ...props}: TProps) => {
  const {Name, Disciplines, DisciplineId, IsActive} = props
  return <>
    <Grid item container xs={12}>
      <Grid item xs={12} component={FormControl}>
        <InputLabel shrink htmlFor="Name">
          Название:
        </InputLabel>
        <Input
          id="Name"
          value={Name}
          name='Name'
          onChange={handleSearch}
          autoFocus={!props.IsMobile}
          fullWidth
        />
      </Grid>
      <MtBlock value={2}/>
      <Grid item container xs={8} wrap='nowrap' zeroMinWidth component={FormControl}>
        <InputLabel>Дисциплина:</InputLabel>
        <Select autoWidth
                value={DisciplineId}
                onChange={handleInput()}
                inputProps={{name: 'DisciplineId', id: 'DisciplineId'}}
                input={<Input/>}
        >
          <MenuItem value={0}>Любая</MenuItem>
          {Disciplines.map(d => <MenuItem key={d.Id} value={d.Id}>{d.Abbreviation}</MenuItem>)}
        </Select>
      </Grid>
      <MrBlock value={2}/>
      <Grid item container xs wrap='nowrap' zeroMinWidth component={FormControl}>
        <InputLabel>Aктивные:</InputLabel>
        <Select autoWidth
                value={IsActive ? 1 : 0}
                onChange={e => {
                  handleInput()({target: {name: 'IsActive', value: !!e.target.value}})
                }}
                input={<Input/>}
        >
          <MenuItem value={1}>Да</MenuItem>
          <MenuItem value={0}>Нет</MenuItem>
        </Select>
      </Grid>
    </Grid>
  </>
}

export default withStyles(styles)(Filter) as FunctionComponent<IProps>