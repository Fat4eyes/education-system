import {createMuiTheme} from '@material-ui/core'

const base = {
  typography: {
    useNextVariants: true
  },
  mixins: {
    toolbar: {
      minHeight: 48,
      padding: '5px 5px'
    }
  }
};

export const pallete = {
  primary: {
    light: '#8ebce0',
    main: '#5d8cae',
    dark: '#2c5f7f',
    contrastText: '#fafafa',
  },
  secondary: {
    light: '#d4d4d4',
    main: '#6e6e6e',
    dark: '#3f3f3f',
    contrastText: '#b2b5c5',
  }
}

export const blue = () => createMuiTheme({
  ...base,
  palette: {
    ...pallete
  },
})