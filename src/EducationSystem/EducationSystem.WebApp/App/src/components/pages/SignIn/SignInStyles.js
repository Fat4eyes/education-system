const styles = theme => ({
  root: {
    // height: `calc(100vh - ${theme.mixins.toolbar.minHeight - theme.spacing.unit}px)`
    paddingTop: '10vh',
  },
  form: {
    height: 'min-content',
    // margin: 'auto',
    // [theme.breakpoints.between('xs', 'md')]: {
    //     //   margin: '10vh auto auto',
    //     //   backgroundColor: 'red'
    //     // }
  },
  marginAuto: {
    margin: 'auto'
  },
  label: {
    width: 'min-content',
    margin: 'auto'
  }
})

export default styles