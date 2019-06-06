import * as React from 'react'
import {FunctionComponent} from 'react'
import {
  createStyles,
  Divider,
  Grid,
  IconButton,
  LinearProgress,
  Theme,
  Tooltip,
  Typography,
  withStyles,
  WithStyles
} from '@material-ui/core'
import Block from '../../../Blocks/Block'
import {Link} from 'react-router-dom'
import {routes} from '../../../Layout/Routes'
import PlayIcon from '@material-ui/icons/PlayArrow'
import RefreshIcon from '@material-ui/icons/Refresh'
import Test from '../../../../models/Test'
import {MtBlock} from '../../../stuff/Margin'
import {lighten} from '@material-ui/core/styles/colorManipulator'
import Button from '../../../stuff/Button'

const styles = (theme: Theme) => createStyles({
  root: {
    padding: theme.spacing.unit * 2
  }
})

const Progress = withStyles({
  root: {
    height: 10,
    backgroundColor: lighten('#24b615', 0.7)
  },
  bar: {
    borderRadius: 20,
    backgroundColor: '#24b615'
  }
})(LinearProgress)

const DividerInternal = withStyles({
  root: {
    width: '100%'
  }
})(Divider)

interface IProps {
  test: Test
  resetTestProcess: (test: Test) => void
}

type TProps = IProps & WithStyles<typeof styles>

const BlockWithProgress = (props: { text: string, value: number}) =>
  <Grid item xs={12} container alignItems='center'>
    <Grid item xs={2}>
      <Typography align='center' color='inherit'>
        {props.text}
      </Typography>
    </Grid>
    <Grid item xs>
      <Progress
        variant='determinate'
        value={props.value}
      />
    </Grid>
  </Grid>

const calcProgressValue = (a: number = 0, b: number) => Math.round(((a || 0) / (b || 1)) * 100)

const TestBlock: FunctionComponent<TProps> = ({classes, test, resetTestProcess}: TProps) => {
  const themesValue = calcProgressValue(test.PassedThemesCount, test.ThemesCount)
  const questionsValue = calcProgressValue(test.PassedQuestionsCount, test.QuestionsCount)
  
  return <Grid item xs={12}>
    <Block partial>
      <Grid container className={classes.root}>
        <Grid item xs={12} container justify='center'>
          <Grid item xs={12} container zeroMinWidth wrap='nowrap' justify='center' alignItems='center'>
            <Typography align='center' color='inherit' noWrap>
              <b>{test.Subject}</b>
            </Typography>
          </Grid>
          <MtBlock/>
          <BlockWithProgress text='Темы:' value={themesValue}/>
          <MtBlock value={0.3}/>
          <DividerInternal/>
          <MtBlock value={0.3}/>
          <BlockWithProgress text='Вопросы:' value={questionsValue}/>
          <Grid item>
            <Tooltip title='Начать обучение' placement='left'>
              <IconButton component={(p: any) => <Link to={routes.studentTest(test.Id!)} {...p}/>}>
                <PlayIcon/>
              </IconButton>
            </Tooltip>
          </Grid>
          <Grid item>
            <Tooltip title='Сбросить прогресс' placement='right'>
              <IconButton onClick={() => resetTestProcess(test)}>
                <RefreshIcon/>
              </IconButton>
            </Tooltip>
          </Grid>
        </Grid>
      </Grid>
    </Block>
  </Grid>
}

export default withStyles(styles)(TestBlock) as FunctionComponent<IProps>