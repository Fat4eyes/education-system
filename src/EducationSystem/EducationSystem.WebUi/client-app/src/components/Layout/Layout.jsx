import React from 'react'
import Router from './Router/Router'
import Header from './Header/Header'
import Footer from './Footer/Footer'

import './layout.less'

const Layout = () => <>
	<div className='page-wrapper'>
		<Router>
			<Header/>
		</Router>
	</div>
	<Footer/>
</>;

export default Layout