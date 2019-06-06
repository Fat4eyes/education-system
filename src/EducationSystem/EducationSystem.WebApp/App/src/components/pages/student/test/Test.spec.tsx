import * as React from 'react'
import {shallow} from 'enzyme'
import Test from './Test'

describe('Test', () => {
  it('should ', function() {
    const wrapper = shallow(<Test/>)
    const state = wrapper.instance().state;
  })
})