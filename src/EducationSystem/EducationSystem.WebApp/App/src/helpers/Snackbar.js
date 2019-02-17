import Exception from './Exception'

const anchorParam = {
  br: {
    vertical: 'bottom',
    horizontal: 'right'
  }
}

class Snackbar {
  constructor(enqueueSnackbar) {
    this.enqueueSnackbar = enqueueSnackbar
  }


  Error = (e = '') => {
    if (e instanceof Exception) {
      e = e.message
    }
    
    return this.enqueueSnackbar(e.toString(), {
      variant: 'error',
      anchorOrigin: anchorParam.br
    })
  }
}

export default Snackbar