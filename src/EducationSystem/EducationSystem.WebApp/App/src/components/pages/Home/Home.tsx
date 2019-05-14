import React from 'react'

class Home extends React.Component {
  componentDidMount(): void {
    document.title = 'Система обучения - Главная'
  }

  render() {
    return <div style={{
      backgroundImage: 'url("https://zakazposterov.ru/fotooboi/z/fotooboi-e-31025-stariy-persidskiy-kover-s-uzorom-vid-sverhu-zakazposterov-ru_z.jpg   ")',
      width: '95vw',
      height: '80vh'
    }}/>
  }
}

export default Home