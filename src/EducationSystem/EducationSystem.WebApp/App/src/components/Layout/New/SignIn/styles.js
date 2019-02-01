const styles = theme => ({
  root: {
    top: `50%`,
    left: `50%`,
    transform: `translate(-50%, -50%)`,
    position: 'absolute',
    maxWidth: 400,
    width: '50%',
    backgroundColor: theme.palette.background.paper,
    boxShadow: theme.shadows[5],
    padding: `${theme.spacing.unit * 2}px ${theme.spacing.unit * 3}px ${theme.spacing.unit * 3}px`,
    outline: 'none',
    flexDirection: 'column',
    alignItems: 'center',
    display: 'flex',
  },
  header: {
    marginBottom: theme.spacing.unit * 3,
  },
  avatar: {
    margin: theme.spacing.unit,
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    display: 'contents',
    width: '100%',
    marginTop: theme.spacing.unit,
  },
  submit: {
    marginTop: theme.spacing.unit * 3,
  }
});

export default styles