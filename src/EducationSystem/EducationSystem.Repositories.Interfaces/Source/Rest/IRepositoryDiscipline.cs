using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    using EducationSystem.Models.Source.Filters;

    public interface IRepositoryDiscipline : IRepositoryReadOnly<DatabaseDiscipline>
    {
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(Filter options);
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplinesByStudentId(int studentId, Filter filter);

        DatabaseDiscipline GetDisciplineById(int id);
    }
}