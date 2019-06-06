import * as React from 'react'
import {FunctionComponent, useEffect} from 'react'
import {Chip, createStyles, Grid, Theme, Typography, withStyles, WithStyles} from '@material-ui/core'
import Block from '../../../Blocks/Block'
import Material, {IMaterialAnchor} from '../../../../models/Material'
import {headerStyles} from '../../../stuff/CommonStyles'
import {MtBlock} from '../../../stuff/Margin'
import {ReadOnlyEditor} from '../../../MaterialEditor/MaterialEditor'
import DocumentFile from '../../../../models/DocumentFile'
import DownloadIcon from '@material-ui/icons/CloudDownload'
import {moveToAnchor} from '../../../MaterialEditor/MaterialEditorTools'
import AnchorIcon from '@material-ui/icons/StarBorder'

const styles = (theme: Theme) => createStyles({
  ...headerStyles(theme),
  root: {},
  chip: {
    margin: theme.spacing.unit * 0.5,
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
  model: Material,
  anchors: Array<IMaterialAnchor>
}

type TProps = IProps & WithStyles<typeof styles>

const MaterialBlock: FunctionComponent<TProps> = ({classes, model, anchors = []}: TProps) => {
  model.Files = model.Files || []

  useEffect(() => {anchors.length === 1 && moveToAnchor(anchors[0].Token)}, [anchors])

  return <Block partial>
    <Grid item xs={12} container className={classes.header} zeroMinWidth wrap='nowrap'>
      <Typography variant='subtitle1' className={classes.headerText} noWrap>
        {model.Name}
      </Typography>
    </Grid>
    <MtBlock value={4}/>
    <Grid item xs={12} container zeroMinWidth>
      {
        anchors.length > 1 && <>
          <Grid item xs={12} container>
            {anchors.map((anchor: IMaterialAnchor) =>
              <Chip key={anchor.Token} className={classes.chip} icon={<AnchorIcon/>} variant='outlined'
                    label={
                      <Grid item xs={12} container zeroMinWidth wrap='nowrap'>
                        <Typography noWrap>
                          {anchor.Name}
                        </Typography>
                      </Grid>
                    }
                    onClick={() => moveToAnchor(anchor.Token)}
              />
            )}
          </Grid>
          <MtBlock value={4}/>
        </>

      }
      <Grid item xs={12}>
        <ReadOnlyEditor html={model.Template}/>
      </Grid>
      {
        model.Files.length && <>
          <MtBlock value={4}/>
          <Grid item xs={12} container>
            {model.Files.map((file: DocumentFile, index: number) =>
              <Chip
                key={file.Id || index}
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
            )}
          </Grid>
        </>
      }
    </Grid>
  </Block>
}

export default withStyles(styles)(MaterialBlock) as FunctionComponent<IProps>