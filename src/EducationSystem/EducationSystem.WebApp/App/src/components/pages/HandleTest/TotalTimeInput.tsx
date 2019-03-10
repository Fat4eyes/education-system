import {ChangeEvent, Component} from 'react'
import * as React from 'react'
import VMaskedField from '../../stuff/VMaskedField'

interface IProps {
  value: number,
  valueHandler: (event: ChangeEvent<HTMLInputElement>) => void
}

class TotalTimeInput extends Component<IProps> {
  handleTime = ({target: {name, value}}: ChangeEvent<HTMLInputElement>) => {
    
    let time = value
    
    this.props.valueHandler({
      target: {
        name,
        value
      }
    } as ChangeEvent<HTMLInputElement>)
  }
  
  render() {
    return               <VMaskedField
      id='TotalTime' name='TotalTime' label='Длительность теста'
      value={this.props.value}
      required
      type='duration'
      validators={{
        max: {value: 3600, message: 'Тест не может быть больше 60 минут'}
      }}
      mask={[/[0-9]/, /[0-9]/, ':', /[0-5]/, /[0-9]/]}
      styles={{}}
    />
  }
}