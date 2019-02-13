import React from 'react'
import {Grid, IconButton, Select, Typography} from '@material-ui/core'
import MenuItem from '@material-ui/core/MenuItem'
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft'
import ChevronRightIcon from '@material-ui/icons/ChevronRight'
import If from '../If'
import PropTypes from 'prop-types'
import withStyles from '@material-ui/core/styles/withStyles'

const TablePagination = (props) => {
  const {
    count,
    page,
    onPageChange,
    onCountPerPageChange,
    showChangeCountPerPageBlock,
    classes
  } = props
  const leftPage = page > 0 ? page - 1 : 0
  const rightPage = page + 1 < count.all / count.perPage ? page + 1 : page

  return <Grid container alignItems='center' spacing={16} className={classes.root}>
    <If condition={!!count.current} orElse={<Grid item xs/>}>
      <If condition={!showChangeCountPerPageBlock}>
        <Grid item>
          <Typography variant='subtitle1' className={classes.typography}>
            Количество записей на странице
          </Typography>
        </Grid>
        <Grid item>
          <Select variant='filled' value={count.perPage} onChange={onCountPerPageChange} className={classes.select}>
            <MenuItem value={10}>10</MenuItem>
            <MenuItem value={25}>25</MenuItem>
            <MenuItem value={50}>50</MenuItem>
          </Select>
        </Grid>
      </If>
      <Grid item xs/>
      <Grid item>
        <Typography variant='subtitle1' className={classes.typography}>
          {`${(page * 10 + 1)}-${(page * 10 + count.current)} из ${count.all}`}
        </Typography>
      </Grid>
      <Grid item>
        <IconButton disabled={count.perPage > count.all} onClick={() => page !== leftPage && onPageChange(leftPage)}>
          <ChevronLeftIcon/>
        </IconButton>
        <IconButton disabled={count.perPage > count.all} onClick={() => page !== rightPage && onPageChange(rightPage)}>
          <ChevronRightIcon/>
        </IconButton>
      </Grid>
    </If>
  </Grid>
}

TablePagination.defaultProps = {
  showChangeCountPerPageBlock: false
}

TablePagination.propTypes = {
  count: PropTypes.shape({
    all: PropTypes.number.isRequired,
    perPage: PropTypes.number.isRequired,
    current: PropTypes.number.isRequired
  }).isRequired,
  page: PropTypes.number.isRequired,
  onPageChange: PropTypes.func.isRequired,
  onCountPerPageChange: PropTypes.func.isRequired,
  showChangeCountPerPageBlock: PropTypes.bool
}

const styles = theme => ({
  root: {
    padding: `0 ${theme.spacing.unit}px`
  },
  typography: {
    color: theme.palette.secondary.dark
  },
  select: {
    '&::before': {
      borderBottom: 'none'
    }
  }
})

export default withStyles(styles)(TablePagination)