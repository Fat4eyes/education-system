import React from 'react'
import {InputBase, withStyles} from '@material-ui/core'
import SearchIcon from '@material-ui/icons/Search'

const Search = ({classes, ...rest}) => <div className={classes.search}>
  <div className={classes.searchIcon}>
    <SearchIcon/>
  </div>
  <InputBase
    placeholder="Название…"
    classes={{
      root: classes.inputRoot,
      input: classes.inputInput
    }}
    {...rest}
  />
</div>

const styles = theme => ({
  search: {
    position: 'relative',
    borderRadius: theme.shape.borderRadius,
    backgroundColor: theme.palette.grey['300'],
    '&:hover': {
      backgroundColor: theme.palette.grey['200']
    },
    width: '100%',
  },
  searchIcon: {
    color: theme.palette.secondary.dark,
    width: theme.spacing.unit * 9,
    height: '100%',
    position: 'absolute',
    pointerEvents: 'none',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center'
  },
  inputRoot: {
    color: 'inherit',
    width: '100%'
  },
  inputInput: {
    paddingTop: theme.spacing.unit,
    paddingRight: theme.spacing.unit,
    paddingBottom: theme.spacing.unit,
    paddingLeft: theme.spacing.unit * 10,
    transition: theme.transitions.create('width'),
    width: '100%',
    [theme.breakpoints.up('md')]: {
      width: 200
    }
  }
})

export default withStyles(styles)(Search)