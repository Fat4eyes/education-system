using System.Collections.Generic;
using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    using EducationSystem.Models.Source.Filters;

    public interface IRepositoryDiscipline : IRepositoryReadOnly<DatabaseDiscipline>
    {
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(FilterDiscipline filter);
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplinesForStudent(int studentId, FilterDiscipline filter);
    }
}