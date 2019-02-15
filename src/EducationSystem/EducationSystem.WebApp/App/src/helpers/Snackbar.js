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


  Error = (message = '') => {
    return this.enqueueSnackbar(message.toString(), {
      variant: 'error',
      anchorOrigin: anchorParam.br
    })
  }
}

export default Snackbar