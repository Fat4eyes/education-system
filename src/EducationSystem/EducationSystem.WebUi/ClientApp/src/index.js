import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import './index.css';
import React from 'react';
import ReactDOM from 'react-dom';
import {BrowserRouter} from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

const Router = () => 
	<BrowserRouter basename={baseUrl}>
		<App/>
	</BrowserRouter>;


ReactDOM.render(<Router/>, document.getElementById('root'));

registerServiceWorker();