using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryTest : IRepositoryReadOnly<DatabaseTest>
    {
        (int Count, List<DatabaseTest> Tests) GetTests(OptionsTest options);

        (int Count, List<DatabaseTest> Tests) GetTestsByDisciplineId(int disciplineId, OptionsTest options);

        DatabaseTest GetTetsById(int id, OptionsTest options);
    }
}