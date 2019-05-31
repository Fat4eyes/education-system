import * as React from 'react'
import {FunctionComponent, PropsWithChildren, ReactNode} from 'react'
import withWidth, {isWidthDown, WithWidth} from '@material-ui/core/withWidth/withWidth'

type TProps = PropsWithChildren<WithWidth>

export const IsMobile = withWidth()(({width, children}: TProps) => {
  return <>{isWidthDown('sm', width) && children}</>
}) as FunctionComponent<PropsWithChildren<{}>>

export const IsNotMobile = withWidth()(({width, children}: TProps) => {
  return <>{!isWidthDown('sm', width) && children}</>
}) as FunctionComponent<PropsWithChildren<{}>>

export const IsMobileAsFuncChild = withWidth()(
  ({width, children}: WithWidth & {children: (isMobile: boolean) => any}) => 
    children(isWidthDown('sm', width))
) as FunctionComponent<PropsWithChildren<{}>>