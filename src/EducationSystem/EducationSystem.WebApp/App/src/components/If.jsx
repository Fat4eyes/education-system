const If = ({condition, children, orElse}) => {
  if (condition) {
    return children
  }
  
  if (orElse) {
    return orElse
  } 
  
  return null
}

export default If