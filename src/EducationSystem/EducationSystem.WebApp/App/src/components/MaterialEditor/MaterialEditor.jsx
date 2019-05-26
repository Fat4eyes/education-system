import React, {Component} from 'react'
import Editor from 'draft-js-plugins-editor'
import MaterialEditorStyles from './MaterialEditorStyles'
import withStyles from '@material-ui/core/styles/withStyles'
import {AtomicBlockUtils, convertFromRaw, convertToRaw, EditorState, Modifier, RichUtils} from 'draft-js'
import {Paper} from '@material-ui/core'
import withWidth from '@material-ui/core/withWidth'
import Grid from '@material-ui/core/Grid'
import {staticToolbarPlugin} from './StaticToolbar/StaticToolbar'
import {AlignmentTool, plugins as imagePlugins} from './Image/Image'
import 'draft-js-alignment-plugin/lib/plugin.css'
import 'draft-js-focus-plugin/lib/plugin.css'
import {MtBlock} from '../stuff/Margin'
import Scrollbar from '../stuff/Scrollbar'
import {getAnchor, setAnchor} from './MaterialEditorTools'
import {withNotifier} from '../../providers/NotificationProvider'
import './anchor.less'
import {Guid} from '../../helpers/guid'
import EditorToolbar from './EditorToolbar'

@withWidth()
@withStyles(MaterialEditorStyles)
@withNotifier
class MaterialEditor extends Component {
  constructor(props) {
    super(props)
    this.state = {
      editorState: !this.props.import
        ? EditorState.createEmpty()
        : EditorState.createWithContent(convertFromRaw(JSON.parse(this.props.import))),
      isAnchorMenuOpen: false,
      anchorName: ''
    }
  }

  handleChange = editorState => this.setState({
    editorState
  }, () => {
    if (this.props.export) {
      let json = JSON.stringify(convertToRaw(editorState.getCurrentContent()))
      this.props.export(json)
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
      {src: `${window.location.origin}/${fileModel.Path}`})
    const entityKey = contentStateWithEntity.getLastCreatedEntityKey()
    const newEditorState = EditorState.set(
      this.state.editorState,
      {currentContent: contentStateWithEntity}
    )
    this.setState({editorState: AtomicBlockUtils.insertAtomicBlock(newEditorState, entityKey, ' ')})
  }

  handleAnchor = {
    open: () => this.setState({isAnchorMenuOpen: true}),
    close: () => this.setState({isAnchorMenuOpen: false}),
    change: ({target: {value}}) => this.setState({anchorName: value}),
    save: () => {
      const anchor = {
        Name: Guid.create(),//this.state.anchorName,
        Token: getAnchor(this.state.editorState)
      }

      if (!anchor.Name.length) this.props.notifier.error('Введите название якоря')
      else {
        this.props.setAnchor && this.props.setAnchor(anchor)
        this.handleAnchor.close()
      }
    }
  }

  handleSetAnchor = anchorName => {
    if (!anchorName.length)
      return this.props.notifier.error('Введите название якоря')

    const anchor = {
      Name: anchorName,
      Token: getAnchor(this.state.editorState)
    }

    this.props.setAnchor && this.props.setAnchor(anchor)
    
    return true
  }

  handleRemoveAnchor = token => {
    this.props.removeAnchor && this.props.removeAnchor(token)
  }
  
  componentDidMount() {
    setAnchor(this.props.materialAnchors, this.props.removeAnchor)
  }

  componentDidUpdate(oldProps) {
    setAnchor(this.props.materialAnchors, this.props.removeAnchor)
  }

  render() {
    const {classes} = this.props
    const plugins = [staticToolbarPlugin, ...imagePlugins]

    return <>
      <Grid item xs={12}>
        <Paper className={classes.toolbarPaper}>
          <EditorToolbar
            editorState={this.state.editorState}
            onLoadImage={this.handleLoadImage}
            onSetAnchor={this.handleSetAnchor}
            onRemove={this.handleRemoveAnchor}
          />
        </Paper>
      </Grid>
      <MtBlock/>
      <Grid item xs={12} container wrap='nowrap' zeroMinWidth>
        <Paper className={classes.root} onClick={() => this.editor.focus()}>
          <AlignmentTool/>
          <Scrollbar className={classes.scrollbar}>
            <div className={classes.editor}>
              <Editor
                onTab={this.handleTab}
                editorState={this.state.editorState}
                onChange={this.handleChange}
                plugins={plugins}
                ref={element => this.editor = element}
              />
            </div>
          </Scrollbar>
        </Paper>
      </Grid>
    </>
  }
}

export default MaterialEditor

export const ReadOnlyEditor = ({html}) => {
  const createState = html => EditorState.createWithContent(convertFromRaw(JSON.parse(html)))
  const plugins = [staticToolbarPlugin, ...imagePlugins]
  return <Editor
    editorState={createState(html)}
    onChange={() => {}}
    plugins={plugins}
    readOnly
  />
}