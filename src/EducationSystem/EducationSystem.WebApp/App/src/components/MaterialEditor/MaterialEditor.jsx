import React, {Component} from 'react'
import MaterialEditorStyles from './MaterialEditorStyles'
import withStyles from '@material-ui/core/styles/withStyles'
import {EditorState, Modifier, RichUtils} from 'draft-js'
import Editor from 'draft-js-plugins-editor'
import {Paper} from '@material-ui/core'
import withWidth from '@material-ui/core/withWidth'
import Grid from '@material-ui/core/Grid'
import {StaticToolbar, staticToolbarPlugin} from './StaticToolbar/StaticToolbar'
import {stateToHTML} from 'draft-js-export-html'
import {stateFromHTML} from 'draft-js-import-html'

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
  
  componentWillReceiveProps(nextProps) {
    this.setState()
  }

  handleChange = editorState => this.setState({editorState}, () => {
    if (this.props.export) {
      let html = stateToHTML(this.state.editorState.getCurrentContent())

      this.props.export(html === EmptyHtmlString ? '' : html)
    }
  })
    
  handleFocus = () => this.editor.focus()

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


  render() {
    const {classes} = this.props

    const getToolbarWidth = () => {
      if (this.toolbarRef.current) {
        console.log(this.toolbarRef.current.clientWidth)

        return this.toolbarRef.current.clientWidth
      } else
        return 52
    }

    return <>
      <Grid item>
        <div style={{width: getToolbarWidth()}}/>
        <div className={classes.toolbar} ref={this.toolbarRef}>
          <Paper className={classes.toolbarPaper}>
            <StaticToolbar/>
          </Paper>
        </div>
      </Grid>
      <Grid item xs container wrap='nowrap' zeroMinWidth>
        <Paper className={classes.root} onClick={this.handleFocus}>
          <Editor
            onTab={this.handleTab}
            handleKeyCommand={this.handleKeyCommand}
            keyBindingFn={this.keyBinding}
            editorState={this.state.editorState}
            onChange={this.handleChange}
            plugins={[
              staticToolbarPlugin
            ]}
            ref={element => this.editor = element}
          />
        </Paper>
      </Grid>
    </>
  }
}

MaterialEditor.defaultProps = {}

MaterialEditor.propTypes = {}

export default MaterialEditor