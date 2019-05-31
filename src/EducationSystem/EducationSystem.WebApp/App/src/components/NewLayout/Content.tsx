import * as React from 'react'
import {FunctionComponent} from 'react'
import {createStyles, Theme, withStyles, WithStyles} from '@material-ui/core'
import Scrollbar from '../stuff/Scrollbar'
import Routes from '../Layout/Routes'
import {headerHeight} from './Layout'
import {getBaseContentPadding, onMobile} from '../stuff/CommonStyles'

const styles = (theme: Theme) => createStyles({
  scrollbar: {
    backgroundColor: '#eaeff1',
    minHeight: `calc(100vh - ${headerHeight}px)`
  },
  content: {
    padding: getBaseContentPadding(theme),
    ...onMobile(theme)({
      padding: `${getBaseContentPadding(theme)}px 0`
    })
  }
})

interface IProps {
}

type TProps = IProps & WithStyles<typeof styles>

const Content: FunctionComponent<TProps> = ({classes}: TProps) => {
  return <Scrollbar className={classes.scrollbar}>
    <main className={classes.content}>
      <Routes/>
    </main>
  </Scrollbar>
}

export default withStyles(styles)(Content) as FunctionComponent<IProps>