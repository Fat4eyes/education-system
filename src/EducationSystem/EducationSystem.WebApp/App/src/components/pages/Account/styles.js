const styles = theme => ({
  grid: {},
  icon: {
    verticalAlign: 'middle',
    paddingRight: theme.spacing.unit * 2
  },
  textWithIcon: {
    verticalAlign: 'middle',
  },
  paper: {
    padding: theme.spacing.unit * 3,
    backgroundColor: theme.palette.grey['50'],
  },
  divider: {
    margin: `${theme.spacing.unit}px auto`,
  },
  text: {
    fontSize: '12pt'
  },
  header: {
    backgroundColor: theme.palette.primary.main,
    padding: `${theme.spacing.unit}px 0 !important`,
    color: theme.palette.primary.contrastText
  },
  body: {
    backgroundColor: theme.palette.grey['50'],
    padding: `${theme.spacing.unit}px 0 !important`
  },
  mt2Unit: {
    marginTop: theme.spacing.unit * 2
  }
});

export default styles