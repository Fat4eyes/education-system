import {number} from 'prop-types'

export const shuffle = (array: Array<any>) => {
  for (let i = array.length - 1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i + 1));
    [array[i], array[j]] = [array[j], array[i]]
  }
}

export const indexOf = <T>(array: Array<T>, predicate: (value: T, index: number) => boolean): number => {
  let indexOf = -1
  if (!array.length) return indexOf
  array.forEach(((v: T, i: number) => {
    if (predicate(v, i)) {
      indexOf = i
      return
    }
  }))
  return indexOf
}
