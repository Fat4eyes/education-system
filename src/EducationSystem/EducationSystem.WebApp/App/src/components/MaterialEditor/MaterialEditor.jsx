import React, {Component} from 'react'
import Editor from 'draft-js-plugins-editor'
import MaterialEditorStyles from './MaterialEditorStyles'
import withStyles from '@material-ui/core/styles/withStyles'
import {AtomicBlockUtils, EditorState, Modifier, RichUtils} from 'draft-js'
import {Paper} from '@material-ui/core'
import withWidth from '@material-ui/core/withWidth'
import Grid from '@material-ui/core/Grid'
import {StaticToolbar, staticToolbarPlugin} from './StaticToolbar/StaticToolbar'
import {stateToHTML} from 'draft-js-export-html'
import {stateFromHTML} from 'draft-js-import-html'
import {AlignmentTool, plugins as imagePlugins} from './Image/Image'
import {FileType} from '../../common/enums'
import FileUpload from '../stuff/FileUpload'
import 'draft-js-alignment-plugin/lib/plugin.css'
import 'draft-js-focus-plugin/lib/plugin.css'

const EmptyHtmlString = '<p><br></p>'

@withWidth()
@withStyles(MaterialEditorStyles)
class MaterialEditor extends Component {
  constructor(props) {
    super(props)
    this.state = {
      editorState: !this.props.import
        ? EditorState.createEmpty()
        : EditorState.createWithContent(stateFromHTML(this.props.import))
    }
    this.toolbarRef = React.createRef()
  }

  handleChange = editorState => this.setState({editorState}, () => {
    if (this.props.export) {
      let html = stateToHTML(this.state.editorState.getCurrentContent())
      this.props.export(html === EmptyHtmlString ? '' : html)
    }
  })
  handleTab = e => {
    e.preventDefault()

    const currentState = this.state.editorState

    const tab = '    '

    const selection = currentState.getSelection()
    const blockType = currentState.getCurrentContent().getBlockForKey(selection.getStartKey()).getType()

    if (blockType === 'unordered-list-item' || blockType === 'ordered-list-item') {
      this.handleChange(RichUtils.onTab(e, currentState, 3))
    } else {
      let newContentState = Modifier.replaceText(currentState.getCurrentContent(), currentState.getSelection(), tab)

      this.handleChange(EditorState.push(currentState, newContentState, 'insert-characters'))
    }
  }
  handleLoadImage = (fileModel) => {
    if (!fileModel || !fileModel.Path) return

    const contentState = this.state.editorState.getCurrentContent()
    const contentStateWithEntity = contentState.createEntity(
      'image',
      'IMMUTABLE',
      {src: `${window.location.origin}/${fileModel.Path}`} )
    const entityKey = contentStateWithEntity.getLastCreatedEntityKey()
    const newEditorState = EditorState.set(
      this.state.editorState,
      {currentContent: contentStateWithEntity}
    )
    this.setState({editorState: AtomicBlockUtils.insertAtomicBlock(newEditorState, entityKey, ' ')})
  }

  render() {
    const {classes} = this.props
    const plugins = [staticToolbarPlugin, ...imagePlugins]

    return <>
      <Grid item xs={12}>
        <Paper className={classes.toolbarPaper}>
          <Grid container justify='center' alignItems='center'>
            <Grid item>
              <StaticToolbar/>
            </Grid>
            <Grid item xs>
              <FileUpload type={FileType.Image} onLoad={this.handleLoadImage}/>
            </Grid>
          </Grid>
        </Paper>
      </Grid>
      <Grid item xs={12} container wrap='nowrap' zeroMinWidth>
        <Paper className={classes.root} onClick={() => this.editor.focus()}>
          <AlignmentTool/>
          <Editor
            onTab={this.handleTab}
            editorState={this.state.editorState}
            onChange={this.handleChange}
            plugins={plugins}
            ref={element => this.editor = element}
          />
        </Paper>
      </Grid>
    </>
  }
}

export default MaterialEditor