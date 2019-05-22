import * as React from 'react'
import {FunctionComponent} from 'react'
import {Chip, createStyles, Grid, Theme, Typography, withStyles, WithStyles} from '@material-ui/core'
import Block from '../../../Blocks/Block'
import Material from '../../../../models/Material'
import {headerStyles} from '../../../stuff/CommonStyles'
import {MtBlock} from '../../../stuff/Margin'
import {ReadOnlyEditor} from '../../../MaterialEditor/MaterialEditor'
import DocumentFile from '../../../../models/DocumentFile'
import DownloadIcon from '@material-ui/icons/CloudDownload'

const styles = (theme: Theme) => createStyles({
  ...headerStyles(theme),
  root: {},
  chip: {
    width: '100%',
    height: 'max-content',
    padding: theme.spacing.unit,
    '&>span': {
      width: '85%',
      padding: `0 ${theme.spacing.unit}px`,
      marginLeft: theme.spacing.unit,
      '&>div>p': {
        whiteSpace: 'initial'
      }
    }
  }
})

interface IProps {
  model: Material
}

type TProps = IProps & WithStyles<typeof styles>

const MaterialBlock: FunctionComponent<TProps> = ({classes, model}: TProps) => {
  model.Files = model.Files || []
  return <Block partial>
    <Grid item xs={12} container className={classes.header} zeroMinWidth wrap='nowrap'>
      <Typography variant='subtitle1' className={classes.headerText} noWrap>
        {model.Name}
      </Typography>
    </Grid>
    <MtBlock value={2}/>
    <Grid item xs={12} container zeroMinWidth>
      <Grid item xs={12} md={9}><ReadOnlyEditor html={model.Template}/></Grid>
      {
        model.Files.length &&
        <Grid item xs={12} md={3} container>
          {model.Files.map((file: DocumentFile, index: number) =>
            <Grid item xs={12} key={file.Id || index} container wrap='nowrap' zeroMinWidth>
              <Chip
                className={classes.chip}
                icon={<DownloadIcon/>}
                label={
                  <Grid item xs={12} container zeroMinWidth wrap='nowrap'>
                    <Typography noWrap>
                      {file.Name}
                    </Typography>
                  </Grid>
                }
                onClick={() => window.open('/' + file.Path)}
                variant='outlined'
              />
            </Grid>
          )}
        </Grid>
      }
    </Grid>
  </Block>
}

export default withStyles(styles)(MaterialBlock) as FunctionComponent<IProps>