using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryDiscipline : IRepositoryReadOnly<DatabaseDiscipline>
    {
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(OptionsDiscipline options);

        DatabaseDiscipline GetDisciplineById(int id, OptionsDiscipline options);
    }
}