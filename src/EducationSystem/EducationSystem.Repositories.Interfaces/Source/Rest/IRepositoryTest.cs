using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryTest : IRepositoryReadOnly<DatabaseTest>
    {
        (int Count, List<DatabaseTest> Tests) GetTests(FilterTest filter);

        (int Count, List<DatabaseTest> Tests) GetTestsByDisciplineId(int disciplineId, FilterTest filter);

        DatabaseTest GetTetsById(int id);
    }
}