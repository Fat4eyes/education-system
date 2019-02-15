import React from 'react'
import {Grid, TableCell, TableRow, Typography, withStyles} from '@material-ui/core'
import ThemesTable from './ThemesTable'
import {Try} from '../../../../core'

const TestDetails = ({test, classes, ...props}) => <>
  <TableRow className={classes.row}>
    <TableCell className={classes.cell}>
      <Grid container className={classes.root} justify='flex-end'>
        <Grid item xs={12} container alignItems='center' className={classes.detailsBlock}>
          {/*TODO Информация по тесту, управляющие кнопки, что-то еще?*/}
          <Typography variant='subtitle1'>
            {test.Subject}
          </Typography>
        </Grid>
        <Grid item xs={12} container className={classes.themesGridBlock}>
          <ThemesTable TestId={test.Id}/>
        </Grid>
      </Grid>
    </TableCell>
  </TableRow>
</>

const styles = theme => {
  const border = `solid 1px ${theme.palette.grey['200']}`
  
  return ({
    row: {
      backgroundColor: theme.palette.grey['50'] + '!important'
    },
    cell: {
      padding: '0 !important'
    },
    root: {
      padding: '5px 0',
    },
    detailsBlock: {
      padding: '5px 10px 5px',
      marginBottom: 5,
    },
    themesGridBlock: {
      padding: '5px 0',
      borderTop: border,
      borderBottom: border
    }
  })
}

export default withStyles(styles)(TestDetails)