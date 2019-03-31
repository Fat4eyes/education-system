import * as React from 'react'
import {ChangeEvent, Component} from 'react'
import VMaskedField from '../../stuff/VMaskedField'

interface IProps {
  value: number,
  onChange: (event: ChangeEvent<HTMLInputElement> | any) => void,
  [propName: string]: any;
}

export default class TotalTimeInput extends Component<IProps> {
  handleChange = ({target: {name, value}}: ChangeEvent<HTMLInputElement>) => {
    let [m, s] = value.replace(new RegExp('_', 'g'), '0')
      .split(':')
      .map(p => Number(p))

    this.props.onChange({
      target: {
        name: name,
        value: m * 60 + s
      }
    })
  }
  
  handleTimeValue = (): string => {
    let normalize = (value: number) => value < 10 ? '0' + value : value
    return `${normalize(~~((this.props.value % 3600) / 60))}:${normalize(~~this.props.value % 60)}`
  }

  render() {
    return <VMaskedField
      {...this.props}
      value={this.handleTimeValue()}
      onChange={this.handleChange}
    />
  }
}