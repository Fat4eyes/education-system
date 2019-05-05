export interface IResult<T> {
  data: T | undefined | null,
  success: boolean
}

export const getResult = <T>(data?: T | undefined | null): IResult<T> => ({
  data: data,
  success: data !== null && data !== undefined
})