import React from 'react'
import './footer.less'

const Footer = () => <div className="footer">
	<div className="footer-copyright">
		<span>&#x24B8;&nbsp;{(new Date()).getFullYear()}</span>
		<span className="footer-developers">Разработчики:&nbsp;Емельяненко Д.В.,&nbsp;Кулицкий В.С.</span>
	</div>
</div>;

export default Footer