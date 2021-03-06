import React from 'react'
import {Grid, Select, Typography} from '@material-ui/core'
import MenuItem from '@material-ui/core/MenuItem'
import ChevronLeftIcon from '@material-ui/icons/ChevronLeftOutlined'
import ChevronRightIcon from '@material-ui/icons/ChevronRightOutlined'
import {If} from '../core'
import PropTypes from 'prop-types'
import withStyles from '@material-ui/core/styles/withStyles'
import withWidth from '@material-ui/core/withWidth'
import OutlinedInput from '@material-ui/core/OutlinedInput'
import Button from './Button'
import {MrBlock} from './Margin'

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
  const PreviousPage = () => (
    <Button mainColor='blue' size='large'
            disabled={count.perPage > count.all || page === 0}
            onClick={() => page !== leftPage && onPageChange(leftPage)}>
      <ChevronLeftIcon/>
    </Button>
  )

  const rightPage = page + 1 < count.all / count.perPage ? page + 1 : page
  const NextPage = () => (
    <Button mainColor='blue' size='large'
            disabled={count.perPage > count.all || page >= (count.all / count.perPage - 1)}
            onClick={() => page !== rightPage && onPageChange(rightPage)}>
      <ChevronRightIcon/>
    </Button>
  )

  const handleSelect = ({target: {value}}) => onCountPerPageChange(value)

  return <Grid container alignItems='center'>
    <If condition={!!count.current} orElse={<Grid item xs/>}>
      {
        count.all > count.perPage && <>
          <Grid item>
            <PreviousPage/>
          </Grid>
          <MrBlock/>
          <Grid item>
            <NextPage/>
          </Grid>
        </>
      }
      <Grid item xs/>
      <Grid item>
        {
          count.all > count.perPage &&
          <Typography variant='body1' className={classes.typography}>
            {`${page + 1} из ${~~(count.all / count.perPage) + (count.all % count.perPage ? 1 : 0)}`}
          </Typography>
        }
      </Grid>
      {
        showChangeCountPerPageBlock &&
        <Grid item>
          <Select
            input={
              <OutlinedInput margin='dense' labelWidth={0} classes={{
                input: classes.input
              }}/>
            }
            value={count.perPage} onChange={handleSelect}
            className={classes.select}>
            {[10, 25, 50].map(i => <MenuItem key={i} value={i}>{i}</MenuItem>)}
          </Select>
        </Grid>
      }
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
  typography: {
    color: theme.palette.secondary.dark
  },
  select: {
    '&::before': {
      borderBottom: 'none'
    }
  },
  input: {
    paddingTop: 11.5,
    paddingBottom: 11.5
  },
  fab: {
    marginRight: theme.spacing.unit * 1.5
  }
})

export default withWidth()(withStyles(styles)(TablePagination))