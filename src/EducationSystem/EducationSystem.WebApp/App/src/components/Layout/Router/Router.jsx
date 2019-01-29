import React, {Component} from 'react'
import {BrowserRouter, Route, Switch} from 'react-router-dom'

import Home from '../../Home/Home'

import './router.less'

class Router extends Component {
  render() {
    return <BrowserRouter>
      <>
        {this.props.children}
        <div className='content'>
          <Switch>
            <Route exact path='/' component={Home}/>
          </Switch>
        </div>
      </>
    </BrowserRouter>;
  }
}

export default Router