import Test from '../../models/Test'

export default interface ITestService {
  getAll(): Test[],
  get(id: number): Promise<Test>,
  add(test: Test): boolean,
  test(): number
}