import React from 'react'
import {NavLink} from 'react-router-dom'

import './header.less'

const Header = () => <div className='header'>
	<a href='http://www.web-test.ru/admin/main'>Главная</a>
	<a href='http://www.web-test.ru/admin/lecturers'>Преподаватели</a>
	<a href='http://www.web-test.ru/admin/groups'>Группы</a>
	<a href='http://www.web-test.ru/admin/students'>Студенты</a>
	<a href='http://www.web-test.ru/admin/disciplines'>Дисциплины</a>
	<a href='http://www.web-test.ru/admin/tests'>Тесты</a>
	<a href='http://www.web-test.ru/admin/results'>Результаты</a>
	<a href='http://www.web-test.ru/admin/performance'>Успеваемость</a>
	<a href='http://www.web-test.ru/admin/materials'>Материалы</a>
	<NavLink to='/learning' activeClassName='current'>Обучение</NavLink>
</div>;

export default Header