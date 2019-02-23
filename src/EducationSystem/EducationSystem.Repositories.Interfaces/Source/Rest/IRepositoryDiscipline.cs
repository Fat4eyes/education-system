using System.Collections.Generic;
using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    using EducationSystem.Models.Source.Filters;

    public interface IRepositoryDiscipline : IRepositoryReadOnly<DatabaseDiscipline>
    {
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(Filter options);
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplinesByStudentId(int studentId, Filter filter);
    }
}