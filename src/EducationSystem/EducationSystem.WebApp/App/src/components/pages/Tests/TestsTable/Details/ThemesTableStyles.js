const ThemesTableStyles = theme => {
  const loadindHeight = 4
  const border = `solid 1px ${theme.palette.grey['300']}`
  const userSelectNone = {
    '-webkit-touch-callout': 'none',
    '-webkit-user-select': 'none',
    '-khtml-user-select': 'none',
    '-moz-user-select': 'none',
    '-ms-user-select': 'none',
    'user-select': 'none'
  }

  return ({
    root: {
      marginTop: theme.spacing.unit,
      padding: `${theme.spacing.unit}px 0`,
      borderTop: border,
      borderBottom: border
    },
    progress: {
      height: loadindHeight
    },
    row: {
      margin: `${theme.spacing.unit / 2}px 0`
    },
    rowHeader: {
      ...userSelectNone,
      padding: `${theme.spacing.unit}px ${theme.spacing.unit * 2}px`,
      backgroundColor: theme.palette.grey['200'],
      '&:hover': {
        backgroundColor: theme.palette.grey['300']
      }
    }
  })
}

export default ThemesTableStyles