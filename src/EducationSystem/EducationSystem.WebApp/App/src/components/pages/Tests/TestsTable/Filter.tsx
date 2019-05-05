import * as React from 'react'
import {FunctionComponent, useState} from 'react'
import Discipline from '../../../../models/Discipline'
import withWidth, {isWidthDown, WithWidth} from '@material-ui/core/withWidth/withWidth'
import {
  Collapse,
  createStyles,
  FormControl,
  FormControlLabel,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  Switch,
  TextField,
  Theme,
  Typography,
  withStyles,
  WithStyles
} from '@material-ui/core'
import Block from '../../../Blocks/Block'
import BlockHeader from '../../../Blocks/BlockHeader'

const styles = (theme: Theme) => createStyles({
  control: {
    margin: `${theme.spacing.unit * 2}px 0`
  },
  cursor: {
    cursor: 'pointer'
  },
  header: {
    height: 65
  }
})

interface IProps {
  Disciplines: Array<Discipline>,
  DisciplineId: number,
  IsActive: boolean,
  handleInput: any,
  handleSearch: any,
  Name: string
}

type TProps = IProps & WithWidth & WithStyles<typeof styles>

const Filter: FunctionComponent<TProps> =
  ({classes, width, Name, Disciplines, DisciplineId, handleSearch, handleInput, IsActive}: TProps) => {
    const [isOpen, handleCollapse] = useState<boolean>(!isWidthDown('md', width))
    return <Block partial>
      <BlockHeader>
        <Typography noWrap variant='subtitle1' component='span' onClick={() => handleCollapse(!isOpen)}>
          Фильтр
        </Typography> 
      </BlockHeader>
      <Collapse in={isOpen}>
        <Grid item container xs={12}>
          <Grid item xs={12} className={classes.control}>
            <TextField
              label='Название теста'
              placeholder='Название теста (больше 3 символов)'
              value={Name}
              name='Name'
              onChange={handleSearch}
              fullWidth
              margin='none'
            />
          </Grid>
          <Grid item container xs={12} wrap='nowrap' zeroMinWidth
                component={FormControl} className={classes.control}>
            <InputLabel>Дисциплина</InputLabel>
            <Select autoWidth
                    value={DisciplineId}
                    onChange={handleInput()}
                    inputProps={{name: 'DisciplineId', id: 'DisciplineId'}}>
              <MenuItem value={0}>Любая</MenuItem>
              {Disciplines.map(d => <MenuItem key={d.Id} value={d.Id}>{d.Abbreviation}</MenuItem>)}
            </Select>
          </Grid>
          <Grid item xs={12} component={FormControl} className={classes.control}>
            <FormControlLabel label='Только активные' control={
              <Switch checked={IsActive}
                      onChange={handleInput(true)}
                      name='IsActive'
                      color='primary'/>}/>
          </Grid>
        </Grid>
      </Collapse>
    </Block>
  }

export default withStyles(styles)(withWidth()(Filter)) as any