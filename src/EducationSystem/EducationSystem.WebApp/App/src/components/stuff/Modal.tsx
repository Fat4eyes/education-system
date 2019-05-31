import * as React from 'react'
import {FunctionComponent, PropsWithChildren} from 'react'
import {
  createStyles,
  Grid,
  Modal as ModalWindow,
  Theme,
  Typography,
  withStyles,
  WithStyles
} from '@material-ui/core'
import Block from '../Blocks/Block'
import {MrBlock, MtBlock} from './Margin'
import Button from './Button'

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
        <Grid item xs={12} container justify='center'>
          <Grid item>
            <Button mainColor='blue' variant='outlined' fullWidth onClick={onYes}>
              Да
            </Button>
          </Grid>
          <MrBlock value={2}/>
          <Grid item>
            <Button mainColor='blue' variant='outlined' fullWidth onClick={onNo}>
              Нет
            </Button>
          </Grid>
        </Grid>
      </Block>
    </div>
  </ModalWindow>

export default withStyles(styles)(Modal) as FunctionComponent<IProps>

interface IEmptyModalProps {
  isOpen: boolean
  onClose: any,
  width?: string,
  height?: string
}

export const EmptyModal = withStyles(styles)(
  ({classes, isOpen, onClose, children, width = '50vw', height = '50vh'}
  : PropsWithChildren<IEmptyModalProps & WithStyles<typeof styles>>) =>
  <ModalWindow open={isOpen} onClose={onClose}>
    <div className={classes.root} style={{width: width, height: height}}>
      {children}
    </div>
  </ModalWindow>
) as FunctionComponent<PropsWithChildren<IEmptyModalProps>>
