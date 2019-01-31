import {teal} from "@material-ui/core/colors";

let base = {
  typography: {
    useNextVariants: true
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
    primary: {
      main: '#e3f2fd',
    },
    secondary: {
      main: '#e3f2fd',
    },
  }
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

export {
  tealTheme, 
  skyTheme,
  redTheme
}