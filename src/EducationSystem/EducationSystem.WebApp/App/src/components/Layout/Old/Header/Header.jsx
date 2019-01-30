import React, {Component} from 'react'
import {NavLink} from 'react-router-dom'
import './header.less'
import env from "../../../../helpers/env";

class Header extends Component {
  render() {
    return <div className='header'>
      <a className='links-menu'>Меню</a>
      <div className='links'>
        <a className='link' href={env.TestingSystemUrl + 'admin/main'}>Главная</a>
        <a className='link' href={env.TestingSystemUrl + 'admin/lecturers'}>Преподаватели</a>
        <a className='link' href={env.TestingSystemUrl + 'admin/groups'}>Группы</a>
        <a className='link' href={env.TestingSystemUrl + 'admin/students'}>Студенты</a>
        <a className='link' href={env.TestingSystemUrl + 'admin/disciplines'}>Дисциплины</a>
        <a className='link' href={env.TestingSystemUrl + 'admin/tests'}>Тесты</a>
        <a className='link' href={env.TestingSystemUrl + 'admin/results'}>Результаты</a>
        <a className='link' href={env.TestingSystemUrl + 'admin/performance'}>Успеваемость</a>
        <a className='link' href={env.TestingSystemUrl + 'admin/materials'}>Материалы</a>
        <NavLink className='link' to='/' activeClassName='current'>Обучение</NavLink>
      </div>
      <a className='user-name'>Администратор</a>
      <div className='user-menu'>
        <a href={env.TestingSystemUrl + 'admin/setting'}>Администрирование</a>
        <a>Сменить пароль</a>
        <a href={env.TestingSystemUrl + 'logout'}>Выход</a>
      </div>
    </div>;
  }
}


export default Header;