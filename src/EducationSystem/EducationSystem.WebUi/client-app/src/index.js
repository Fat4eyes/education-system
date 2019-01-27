import React from 'react';
import ReactDOM from 'react-dom';
import App from './components/App/App';
import * as serviceWorker from './serviceWorker';

import './index.less'

ReactDOM.render(<App/>, document.getElementById('root'));

serviceWorker.unregister();