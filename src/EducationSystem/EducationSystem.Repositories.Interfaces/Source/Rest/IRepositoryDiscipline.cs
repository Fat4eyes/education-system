using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryDiscipline : IRepositoryReadOnly<DatabaseDiscipline>
    {
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(OptionsDiscipline options);
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplinesByStudentId(int studentId, OptionsDiscipline options);

        DatabaseDiscipline GetDisciplineById(int id, OptionsDiscipline options);
    }
}