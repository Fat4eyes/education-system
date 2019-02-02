using System.Collections.Generic;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    /// <summary>
    /// Интерфейс менеджера по работе с учебными планами.
    /// </summary>
    public interface IStudyPlanManager
    {
        /// <summary>
        /// Возвращает список всех учебных планов.
        /// </summary>
        /// <returns>Список всех учебных планов.</returns>
        List<StudyPlan> GetAll();

        /// <summary>
        /// Возвращает учебный план по указанному идентификатору.
        /// </summary>
        /// <returns>Учебный план.</returns>
        StudyPlan GetById(int id);
    }
}