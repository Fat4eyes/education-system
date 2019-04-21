import * as React from 'react'
import {Component, createContext} from 'react'
import {CircularProgress, createStyles, Theme, withStyles, WithStyles} from '@material-ui/core'

interface ISpinnerProvider {
  set: (value?: boolean) => void
  enable: () => void
  disable: () => void
}

export type TSpinnerProps = {
  spinner: ISpinnerProvider
}

const NullSpinner: ISpinnerProvider = {
  set: (value?: boolean) => {console.log('spinner: ', value)},
  enable: () => {},
  disable: () => {}
}

interface IState {
  IsLoading: boolean
}

const {Provider, Consumer} = createContext(NullSpinner)

export const SpinnerConsumer = Consumer

const styles = (theme: Theme) => createStyles({
  root: {
    backgroundColor: `rgba(255, 254, 254, 0.8)`,
    position: 'fixed',
    top: 58,
    left: 0,
    height: '100%',
    width: '100%',  
    '& > div': {
      opacity: 1,
      position: 'absolute',
      top: `45%`,
      left: `50%`,
      transform: `translate(-45%, -50%)`
    }
  }
})

class SpinnerProvider extends Component<WithStyles<typeof styles>, IState> {
  constructor(props: any) {
    super(props)

    this.state = {
      IsLoading: false
    }
  }

  private _handlers: ISpinnerProvider = {
    set: (value?: boolean) => value === undefined
      ? this.setState(state => ({IsLoading: !state.IsLoading}))
      : this.setState({IsLoading: value}),
    enable: () => this._handlers.set(true),
    disable: () => this._handlers.set(false)
  }

  render(): React.ReactNode {
    return <Provider value={this._handlers}>
      {this.props.children}
      {
        this.state.IsLoading &&
        <div className={this.props.classes.root}>
          <div>
            <CircularProgress size={100} thickness={1}/>
          </div>
        </div>
      }
    </Provider>
  }
}

export const withSpinner = (Component: any) => (props: any) =>
  <Consumer>
    {spinner => <Component {...props} spinner={spinner}/>}
  </Consumer>

export default withStyles(styles)(SpinnerProvider) as any