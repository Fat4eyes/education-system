import * as React from 'react'
import {SortableContainer, SortableElement, SortableHandle} from 'react-sortable-hoc'
import {Grid} from '@material-ui/core'
import ArrowsIcon from '@material-ui/icons/UnfoldMore'
import RowHeader from '../Table/RowHeader'

const DragHandler = SortableHandle(() => <Grid item container alignItems='center'>
  <ArrowsIcon color='action'/>
</Grid>)

export const SortableArrayItem = SortableElement(
  ({value}: any) =>
    <Grid item xs={12} container>
      <RowHeader>
        <Grid item xs={12} container alignItems='center' wrap='nowrap'>
          <Grid item style={{marginRight: 16}}>
            <DragHandler/>
          </Grid>
          <Grid item xs container alignItems='center'>
            {value}
          </Grid>
        </Grid>
      </RowHeader>
    </Grid>
)

export const SortableArrayContainer = SortableContainer(
  ({children}: any) => <Grid container>{children}</Grid>
)