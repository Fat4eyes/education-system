import {important} from '../stuff/CommonStyles'

const MaterialEditorStyles = theme => {
  return ({
    toolbarPaper: {
      padding: theme.spacing.unit
    },
    root: {
      cursor: 'text',
      width: '100%' + important,
    },
    scrollbar: {
      boxSizing: 'border-box',
      height: `calc(80vh - ${theme.mixins.toolbar.minHeight + 10 + theme.spacing.unit}px)` + important,
      width: '100%' + important,
    },
    editor: {
      padding: theme.spacing.unit * 3
    }
  })
}

export default MaterialEditorStyles