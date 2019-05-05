import * as React from 'react'
import {FunctionComponent} from 'react'
import {
  Button,
  createStyles,
  Grid,
  Modal as ModalWindow,
  Theme,
  Typography,
  withStyles,
  WithStyles
} from '@material-ui/core'
import Block from '../Blocks/Block'
import {MtBlock} from './Margin'

export const styles = (theme: Theme) => createStyles({
  root: {
    position: 'absolute',
    width: 300,
    top: `50%`,
    left: `50%`,
    transform: `translate(-50%, -50%)`,
    padding: 1
  },
  header: {
    backgroundColor: theme.palette.primary.main,
    padding: `${theme.spacing.unit}px ${theme.spacing.unit * 3}px !important`,
    color: theme.palette.primary.contrastText
  },
  headerText: {
    color: theme.palette.primary.contrastText
  }
})

interface IProps {
  isOpen: boolean
  onClose: any
  onYes: any
  onNo: any
}

type TProps = WithStyles<typeof styles> & IProps

const Modal: FunctionComponent<TProps> = ({classes, isOpen, onClose, onNo, onYes}) =>
  <ModalWindow open={isOpen} onClose={onClose}>
    <div className={classes.root}>
      <Block partial>
        <Grid item xs={12} className={classes.header} container zeroMinWidth wrap='nowrap'>
          <Typography noWrap variant='subtitle1' className={classes.headerText}>
            Вы уверены?
          </Typography>
        </Grid>
        <MtBlock value={2}/>
        <Grid item xs={12} container spacing={8}>
          <Grid item xs={6}>
            <Button variant='outlined' fullWidth onClick={onYes}>
              Да
            </Button>
          </Grid>
          <Grid item xs={6}>
            <Button variant='outlined' fullWidth onClick={onNo}>
              Нет
            </Button>
          </Grid>
        </Grid>
      </Block>
    </div>
  </ModalWindow>

export default withStyles(styles)(Modal) as FunctionComponent<IProps>