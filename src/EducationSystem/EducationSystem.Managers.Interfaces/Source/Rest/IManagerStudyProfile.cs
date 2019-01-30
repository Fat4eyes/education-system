using System.Collections.Generic;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    /// <summary>
    /// Интерфейс менеджера по работе с профилями обучения.
    /// </summary>
    public interface IManagerStudyProfile
    {
        /// <summary>
        /// Возвращает список всех профилей обучения.
        /// </summary>
        /// <returns>Список всех профилей обучения.</returns>
        List<StudyProfile> GetAll();

        /// <summary>
        /// Возвращает профиль обучения по указанному идентификатору.
        /// </summary>
        /// <returns>Профиль обучения.</returns>
        StudyProfile GetById(int id);
    }
}