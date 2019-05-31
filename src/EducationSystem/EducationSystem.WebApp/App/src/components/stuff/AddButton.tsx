import * as React from 'react'
import {FunctionComponent} from 'react'
import {createStyles, Grid, Theme, Typography, withStyles, WithStyles} from '@material-ui/core'
import RowHeader from '../Table/RowHeader'
import AddIcon from '@material-ui/icons/Add'

const styles = (theme: Theme) => createStyles({
  root: {}
})

interface IProps {
  onClick: () => void
}

type TProps = IProps & WithStyles<typeof styles>

const AddButton: FunctionComponent<TProps> = ({onClick}: TProps) => <RowHeader onClick={onClick}>
  <Grid item xs={12} container alignItems='center' justify='center' wrap='nowrap' zeroMinWidth>
    <AddIcon/>
    <Typography noWrap variant='subtitle1'>
      Добавить
    </Typography>
  </Grid>
</RowHeader>


export default withStyles(styles)(AddButton) as FunctionComponent<IProps>