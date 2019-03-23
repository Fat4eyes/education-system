import Exception from '../../helpers/Exception'
import Theme from '../../models/Theme'

export default interface IThemeService {
  add(theme: Theme): Promise<Theme | Exception>,
}