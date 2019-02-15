const ThemesTableStyles = theme => {
  const loadindHeight = 4
  
  return ({
    root: {
      margin: 'auto'
    },
    table: {
      borderSpacing: `${theme.spacing.unit / 2}px !important`,
      borderCollapse: 'separate !important',
      paddingBottom: loadindHeight,
    },
    cell: {
      padding: 0
    },
    loadingBlock: {
      padding: '0 5px 0 4px',
      height: loadindHeight
    },
    themeHeader: {
      [theme.breakpoints.up('xs')]: {
        width: 150
      },
      [theme.breakpoints.up('sm')]: {
        width: 200
      },
      [theme.breakpoints.up('md')]: {
        width: 400
      },
      [theme.breakpoints.up('lg')]: {
        width: 500
      }
    }
  })
}

export default ThemesTableStyles