import * as React from 'react'
import {FunctionComponent} from 'react'
import {createStyles, Menu, MenuItem, Theme, withStyles, WithStyles} from '@material-ui/core'
import DocumentFile from '../../../models/DocumentFile'
import {PopoverProps} from '@material-ui/core/Popover'

const styles = (theme: Theme) => createStyles({
  root: {}
})

interface IProps {
  documents: Array<DocumentFile>
  onClose: (file?: DocumentFile) => void
  anchorEl: PopoverProps['anchorEl']
  isOpen: boolean
}

type TProps = IProps & WithStyles<typeof styles>

const FileSelect: FunctionComponent<TProps> =
  ({classes, documents, onClose, anchorEl, isOpen}: TProps) =>
    <Menu anchorEl={anchorEl} open={isOpen} onClose={() => onClose()}>
      {documents.map((file: DocumentFile, index: number) =>
        <MenuItem key={file.Id || index} onClick={() => onClose(file)}>
          {file.Name}
        </MenuItem>
      )}
    </Menu>


export default withStyles(styles)(FileSelect) as FunctionComponent<IProps>