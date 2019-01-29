import React from 'react'
import './footer.less'

const Footer = () => <div className="footer">
  <div className="footer-copyright">
    <span>&#x24B8;&nbsp;{(new Date()).getFullYear()}</span>
    <span className="footer-developers">Разработчики:&nbsp;Кулицкий В.С.,&nbsp;Емельяненко Д.В.</span>
  </div>
</div>;

export default Footer