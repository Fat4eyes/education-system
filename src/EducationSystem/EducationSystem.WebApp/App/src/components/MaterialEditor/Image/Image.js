import {composeDecorators} from 'draft-js-plugins-editor'
import createImagePlugin from 'draft-js-image-plugin'
import createAlignmentPlugin from 'draft-js-alignment-plugin'
import createFocusPlugin from 'draft-js-focus-plugin'
import createResizeablePlugin from 'draft-js-resizeable-plugin'
import createBlockDndPlugin from 'draft-js-drag-n-drop-plugin'

const focusPlugin = createFocusPlugin()
const resizeablePlugin = createResizeablePlugin()
const blockDndPlugin = createBlockDndPlugin()
const alignmentPlugin = createAlignmentPlugin()
export const {AlignmentTool} = alignmentPlugin

const decorator = composeDecorators(
  resizeablePlugin.decorator,
  alignmentPlugin.decorator,
  focusPlugin.decorator,
  blockDndPlugin.decorator,
)
const imagePlugin = createImagePlugin({decorator})

export const plugins = [
  blockDndPlugin,
  focusPlugin,
  alignmentPlugin,
  resizeablePlugin,
  imagePlugin
]