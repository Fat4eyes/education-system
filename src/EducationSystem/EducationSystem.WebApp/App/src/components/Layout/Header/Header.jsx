import React, {Component} from 'react'
import {NavLink} from 'react-router-dom'
import {testingSystemUrl} from '../../../env'

import './header.less'

class Header extends Component {
  render() {
    return <div className='header'>
      <a className='links-menu'>Меню</a>
      <div className='links'>
        <a className='link' href={testingSystemUrl + 'admin/main'}>Главная</a>
        <a className='link' href={testingSystemUrl + 'admin/lecturers'}>Преподаватели</a>
        <a className='link' href={testingSystemUrl + 'admin/groups'}>Группы</a>
        <a className='link' href={testingSystemUrl + 'admin/students'}>Студенты</a>
        <a className='link' href={testingSystemUrl + 'admin/disciplines'}>Дисциплины</a>
        <a className='link' href={testingSystemUrl + 'admin/tests'}>Тесты</a>
        <a className='link' href={testingSystemUrl + 'admin/results'}>Результаты</a>
        <a className='link' href={testingSystemUrl + 'admin/performance'}>Успеваемость</a>
        <a className='link' href={testingSystemUrl + 'admin/materials'}>Материалы</a>
        <NavLink className='link' to='/learning' activeClassName='current'>Обучение</NavLink>
      </div>
      <a className='user-name'>Администратор</a>
      <div className='user-menu'>
        <a href={testingSystemUrl + 'admin/setting'}>Администрирование</a>
        <a>Сменить пароль</a>
        <a href={testingSystemUrl + 'logout'}>Выход</a>
      </div>
    </div>;
  }
}

export default Header