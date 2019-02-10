const If = ({condition, children, orElse}) =>
  condition
    ? children
    : orElse
    ? orElse
    : null

export default If