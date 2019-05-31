import * as React from 'react'
import {FunctionComponent} from 'react'
import {createStyles, InputBase, Theme, withStyles} from '@material-ui/core'
import {fade} from '@material-ui/core/styles/colorManipulator'
import {InputBaseProps} from '@material-ui/core/InputBase'

const Input = withStyles((theme: Theme) =>
  createStyles({
    root: {
      'label + &': {
        marginTop: theme.spacing.unit * 3
      },
      padding: 0
    },
    input: {
      borderRadius: 4,
      position: 'relative',
      backgroundColor: theme.palette.common.white,
      border: '1px solid #ced4da',
      fontSize: 16,
      padding: '10px 12px',
      transition: theme.transitions.create(['border-color', 'box-shadow']),
      fontFamily: [
        '-apple-system',
        'BlinkMacSystemFont',
        '"Segoe UI"',
        'Roboto',
        '"Helvetica Neue"',
        'Arial',
        'sans-serif',
        '"Apple Color Emoji"',
        '"Segoe UI Emoji"',
        '"Segoe UI Symbol"'
      ].join(','),
      '&:focus': {
        boxShadow: `${fade(theme.palette.primary.main, 0.25)} 0 0 0 0.2rem`,
        borderColor: theme.palette.primary.main
      }
    }
  })
)(InputBase)

export default Input as FunctionComponent<InputBaseProps>