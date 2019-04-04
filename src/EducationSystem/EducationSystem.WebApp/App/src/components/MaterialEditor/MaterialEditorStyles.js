const MaterialEditorStyles = theme => ({
  toolbar: {
    // position: 'fixed',
    // top: '50%',
    // transform: 'translateY(-50%)'
  },
  toolbarPaper: {
    padding: theme.spacing.unit
  },
  root: {
    width: '100%',
    minHeight: `calc(100vh - ${theme.mixins.toolbar.minHeight + 10 + theme.spacing.unit * 4}px)`,
    padding: theme.spacing.unit * 3,
    boxSizing: 'border-box',
    cursor: 'text'
  }
})

export default MaterialEditorStyles