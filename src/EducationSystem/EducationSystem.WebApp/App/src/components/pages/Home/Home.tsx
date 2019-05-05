import React from 'react'

class Home extends React.Component {
  componentDidMount(): void {
    document.title = 'Система обучения - Главная'
  }

  render() {
    return <div style={{
      backgroundImage: 'url("https://pp.userapi.com/c630818/v630818333/2f4bc/7rFVOXiVrMc.jpg")',
      width: '95vw',
      height: '80vh'
    }}/>
  }
}

export default Home