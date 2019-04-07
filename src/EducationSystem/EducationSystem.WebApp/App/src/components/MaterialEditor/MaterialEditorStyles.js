const MaterialEditorStyles = theme => {
  const height = `calc(80vh - ${theme.mixins.toolbar.minHeight + 10 + theme.spacing.unit * 4}px)`
  
  return ({
    toolbarPaper: {
      padding: theme.spacing.unit
    },
    root: {
      overflowY: 'scroll',
      width: '100%',
      minHeight: height,
      maxHeight: height,
      padding: theme.spacing.unit * 3,
      boxSizing: 'border-box',
      cursor: 'text'
    }
  })
}

export default MaterialEditorStyles