import React from 'react'
import createSideToolbarPlugin from 'draft-js-side-toolbar-plugin';
import {
  BlockquoteButton,
  BoldButton,
  CodeButton,
  ItalicButton,
  OrderedListButton,
  UnderlineButton,
  UnorderedListButton
} from 'draft-js-buttons'
import buttonStyles from './buttonStyles.module.less'
import toolbarStyles from './toolbarStyles.module.less'
import blockTypeSelectStyles from './blockTypeSelectStyles.module.less'

export const sideToolbarPlugin = createSideToolbarPlugin({
  position: 'right',
  theme: {buttonStyles, toolbarStyles, blockTypeSelectStyles}
})

const buttons = [
  BoldButton,
  ItalicButton,
  UnderlineButton,
  CodeButton,
  UnorderedListButton,
  OrderedListButton,
  BlockquoteButton
]

const {SideToolbar: Toolbar} = sideToolbarPlugin
export const SideToolbar = ({...props}) => <Toolbar {...props}>
  {externalProps => <>{
    buttons.map((Button, index) => <Button  key={index} {...externalProps}/>)
  }</>}
</Toolbar>

