import {createMuiTheme} from '@material-ui/core'

const base = {
  typography: {
    useNextVariants: true,
    fontFamily: '"Noto Sans", sans-serif',
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

export const edo = () => createMuiTheme({
  ...base,
  palette: {
    primary: {
      light: '#07b7b4',
      main: '#209387',
      dark: '#036561',
      contrastText: '#ffffff',
    },
    secondary: {
      light: '#e4ffff',
      main: '#b1ddd9',
      dark: '#81aba8',
      contrastText: '#000000',
    }
  },
})

export const purpure = () => createMuiTheme({
  ...base,
  palette: {
    primary: {
      light: '#b48ddf',
      main: '#835fad',
      dark: '#54347d',
      contrastText: '#ffffff',
    },
    secondary: {
      light: '#fff7ff',
      main: '#d1c4e9',
      dark: '#a094b7',
      contrastText: '#000000',
    }
  },
})

export const grey = () => createMuiTheme({
  ...base,
  palette: {
    primary: {
      light: '#a7c0cd',
      main: '#78909c',
      dark: '#4b636e',
      contrastText: '#ffffff',
    },
    secondary: {
      light: '#efefef',
      main: '#bdbdbd',
      dark: '#8d8d8d',
      contrastText: '#000000',
    }
  },
})