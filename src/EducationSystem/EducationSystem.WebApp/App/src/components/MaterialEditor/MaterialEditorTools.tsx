import {EditorState} from 'draft-js'
import * as React from 'react'
import {IMaterialAnchor} from '../../models/Material'

export const getAnchor = (editorState: EditorState) => editorState.getSelection().getAnchorKey()
export const findAnchor = (anchorKey: string) => document.querySelector(`div[data-offset-key^=${CSS.escape(anchorKey)}]`)

export const setAnchor = (anchors: IMaterialAnchor[], onRemove: (token: string) => void) => {
  let anchorElements = document.querySelectorAll(`span[id^=anchor-]`)
  anchorElements.forEach(el => el.remove())
  anchors.forEach((a: IMaterialAnchor) => {
    const anchoredElement = findAnchor(a.Token)
    const anchorElement = document.getElementById(`anchor-${a.Token}`)
    if (!anchoredElement) {
      if (anchorElement) {
        onRemove(a.Token)
        anchorElement.remove()
      }
      return 
    }
    
    const setAnchorInternal = () => {
      let e = document.createElement('span')
      e.id = `anchor-${a.Token}`
      e.className = 'anchorElement'
      e.setAttribute('tooltip', a.Name)
      e.setAttribute('flow', 'right')
      anchoredElement.before(e)
      anchoredElement.className = 'anchoredElement'
    }
    
    if (anchorElement && 
      anchorElement.getBoundingClientRect().top != anchoredElement.getBoundingClientRect().top) {
      anchorElement.remove()
      return setAnchorInternal()
    }

    return setAnchorInternal()
  })
}

export const moveToAnchor = (anchorKey: string) => {
  const contentBlock = findAnchor(anchorKey)
  if (contentBlock) {
    contentBlock.scrollIntoView({
      block: 'center'
    })
  }
}