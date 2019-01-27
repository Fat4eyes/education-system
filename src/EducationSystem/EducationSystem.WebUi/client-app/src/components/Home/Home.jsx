import React, {Component} from 'react'
import {apiRoutes} from '../../env'
import fetch from '../../helpers/fetch'

class Home extends Component {
	constructor(props) {
		super(props);
	}

	async componentDidMount() {
		let response = await fetch(apiRoutes.getAllUsers);
		let result = await response.json();
		console.log(result)
	}

	render() {
		return <>
			Контент
		</>
	}
}

export default Home