import {blue, teal} from "@material-ui/core/colors";

let base = {
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

const tealTheme = {
  ...base,
  palette: {
    primary: teal,
    secondary: {
      main: '#80cbc4',
    },
  }
};

const skyTheme = {
  ...base,
  palette: {
    primary: blue,
    secondary: {
      main: '#2196f3',
    },
  },
};

const redTheme = {
  ...base,
  palette: {
    primary: {
      main: '#e57373',
    },
    secondary: {
      main: '#fce4ec',
    },
  }
};

const baseTheme = {
  ...base,
  palette: {
    primary: {
      light: '#8ebce0',
      main: '#5d8cae',
      dark: '#2c5f7f',
      contrastText: '#fafafa',
    },
    secondary: {
      light: '#898989',
      main: '#6e6e6e',
      dark: '#3f3f3f',
      contrastText: '#b2b5c5',
    },
  },
}
export {
  tealTheme, 
  skyTheme,
  redTheme,
  baseTheme
}