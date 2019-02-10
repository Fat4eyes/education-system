class Mapper {
  static map = (from, basedOn) => {
    const isArray = Array.isArray(from)
    try {
      return isArray
        ? from.map(item => ({...basedOn, ...item}))
        : {...basedOn, ...from}
    } catch (e) {
      console.log(e)

      return isArray 
        ? [] 
        : basedOn
    }
  }
}

export default Mapper