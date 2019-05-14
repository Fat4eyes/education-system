import * as React from 'react'
import {Component, createRef} from 'react'
import Scrollbars from 'react-custom-scrollbars'
import {createStyles, WithStyles, withStyles} from '@material-ui/core'

export const styles = () => createStyles({
  root: {
    position: 'relative',
    width: '100%'
  },
  shadowTop: {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    height: 5,
    background: 'linear-gradient(to bottom, rgba(0, 0, 0, 0.2) 0%, rgba(0, 0, 0, 0) 100%)'
  },
  shadowBottom: {
    position: 'absolute',
    bottom: 0,
    left: 0,
    right: 0,
    height: 5,
    background: 'linear-gradient(to top, rgba(0, 0, 0, 0.2) 0%, rgba(0, 0, 0, 0) 100%)'
  }
})

interface IProps {
  spacing?: number
}

type TProps = WithStyles<typeof styles> & IProps

interface IState {
  scrollTop: number
  scrollHeight: number
  clientHeight: number
}

class Scrollbar extends Component<TProps, IState> {
  private _refs = {
    shadowTop: createRef<HTMLDivElement>(),
    shadowBottom: createRef<HTMLDivElement>()
  }

  handleUpdate = (values: IState) => {
    const {shadowTop: {current: shadowTop}, shadowBottom: {current: shadowBottom}} = this._refs
    const {scrollTop, scrollHeight, clientHeight} = values

    const bottomScrollTop = scrollHeight - clientHeight

    shadowTop!.style.opacity = `${1 / 20 * Math.min(scrollTop, 20)}`
    shadowBottom!.style.opacity = `${1 / 20 * (bottomScrollTop - Math.max(scrollTop, bottomScrollTop - 20))}`
  }

  render() {
    const {classes, spacing} = this.props
    return <div className={classes.root} style={spacing ? {padding: spacing} : undefined}>
      <Scrollbars onUpdate={this.handleUpdate} {...this.props}/>
      <div ref={this._refs.shadowTop} className={classes.shadowTop}/>
      <div ref={this._refs.shadowBottom} className={classes.shadowBottom}/>
    </div>
  }
}

export default withStyles(styles)(Scrollbar) as any