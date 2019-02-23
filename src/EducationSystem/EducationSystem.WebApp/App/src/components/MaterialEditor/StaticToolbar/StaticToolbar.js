import React from 'react'
import createToolbarPlugin from 'draft-js-static-toolbar-plugin'
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

export const staticToolbarPlugin = createToolbarPlugin({
  theme: {buttonStyles, toolbarStyles}
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

const {Toolbar} = staticToolbarPlugin
export const StaticToolbar = ({...props}) => <Toolbar {...props}>
  {externalProps => <>{
    buttons.map((Button, index) => <Button  key={index} {...externalProps}/>)
  }</>}
</Toolbar>

