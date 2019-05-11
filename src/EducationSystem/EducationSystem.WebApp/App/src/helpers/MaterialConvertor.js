import mammoth from 'mammoth'
import Exception from './Exception'

export const MaterialConvertor = async path => {
  if (!path || typeof path !== 'string') throw new Exception('Не верно задан путь')
  
  let extension = path.split('.').reverse()[0]
  
  switch (extension) {
    case 'docx':
      return await docx2htmlConvertor(path)
    case 'pdf':
    default:
      throw new Exception(`Материалы с расширением <.${extension}> не поддерживаются`)
  }
}

const docx2htmlConvertor = async path => {
  let arrayBuffer
  try {
    let request = await fetch(path, {method: 'GET'})
    arrayBuffer = await request.arrayBuffer()
  } catch (e) {
    throw new Exception(e)
  }
  
  return await mammoth.convertToHtml({arrayBuffer: arrayBuffer})
}