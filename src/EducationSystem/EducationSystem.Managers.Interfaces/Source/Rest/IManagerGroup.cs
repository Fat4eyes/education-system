using System.Collections.Generic;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    /// <summary>
    /// Интерфейс менеджера по работе с группами.
    /// </summary>
    public interface IManagerGroup
    {
        /// <summary>
        /// Возвращает список всех групп.
        /// </summary>
        /// <returns>Список всех групп.</returns>
        List<Group> GetAll();

        /// <summary>
        /// Возвращает группу по указанному идентификатору.
        /// </summary>
        /// <returns>Группа.</returns>
        Group GetById(int id);
    }
}