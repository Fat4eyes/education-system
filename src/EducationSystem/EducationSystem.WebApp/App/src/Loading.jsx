import React from 'react'
import {CircularProgress, withStyles} from "@material-ui/core";

const styles = theme => ({
  root: {
    position: 'absolute',
    top: `50%`,
    left: `50%`,
    transform: `translate(-50%, -50%)`,
  },
});

const Loading = ({classes}) => <div className={classes.root}>
  <CircularProgress size={100} thickness={1} />;
</div>;

export default withStyles(styles)(Loading)