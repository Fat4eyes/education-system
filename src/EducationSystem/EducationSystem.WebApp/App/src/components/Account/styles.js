const styles = theme => ({
  root: {
    flexGrow: 1,
    margin: `${theme.spacing.unit}px auto`,
  },
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
  }
});

export default styles