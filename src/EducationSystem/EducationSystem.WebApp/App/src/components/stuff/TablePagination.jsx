import React from 'react'
import {Grid, IconButton, Select, Typography} from '@material-ui/core'
import MenuItem from '@material-ui/core/MenuItem'
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft'
import ChevronRightIcon from '@material-ui/icons/ChevronRight'
import {If} from '../core'
import PropTypes from 'prop-types'
import withStyles from '@material-ui/core/styles/withStyles'
import withWidth, {isWidthDown} from '@material-ui/core/withWidth'

const TablePagination = (props) => {
  const {
    count,
    page,
    onPageChange,
    onCountPerPageChange,
    showChangeCountPerPageBlock,
    classes,
    width
  } = props

  let isXs = isWidthDown('xs', width)

  const leftPage = page > 0 ? page - 1 : 0
  const PreviousPage = () => (
    <IconButton disabled={count.perPage > count.all || page === 0}
                onClick={() => page !== leftPage && onPageChange(leftPage)}>
      <ChevronLeftIcon/>
    </IconButton>
  )

  const rightPage = page + 1 < count.all / count.perPage ? page + 1 : page
  const NextPage = () => (
    <IconButton disabled={count.perPage > count.all || page >= (count.all / count.perPage - 1)}
                onClick={() => page !== rightPage && onPageChange(rightPage)}>
      <ChevronRightIcon/>
    </IconButton>
  )

  return <Grid container alignItems='center' spacing={16} className={classes.root}>
    <If condition={!!count.current} orElse={<Grid item xs/>}>
      <If condition={!showChangeCountPerPageBlock && !isXs}>
        <Grid item>
          <Typography variant='subtitle1' className={classes.typography}>
            Количество записей на странице
          </Typography>
        </Grid>
        <Grid item>
          <Select variant='filled' value={count.perPage} onChange={onCountPerPageChange} className={classes.select}>
            {[12, 25, 50].map(i => <MenuItem key={i} value={i}>{i}</MenuItem>)}
          </Select>
        </Grid>
      </If>
      <Grid item xs/>
      <Grid item>
        <Typography variant='subtitle1' className={classes.typography}>
          {`${(page * count.perPage + 1)}-${(page * count.perPage + count.current)} из ${count.all}`}
        </Typography>
      </Grid>
      <Grid item>
        <PreviousPage/>
        <NextPage/>
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

export default withWidth()(withStyles(styles)(TablePagination))