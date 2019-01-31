import React from 'react'
import {Link} from "react-router-dom";

const SimpleLink = props => <Link {...props} style={{ 
  textDecoration: 'none',
  borderBottom: 'none',
  paddingBottom: '15px'
}}/>;

export default SimpleLink