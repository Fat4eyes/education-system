using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryTest : IRepository<DatabaseTest>
    {
        (int Count, List<DatabaseTest> Tests) GetTests(FilterTest filter);
        (int Count, List<DatabaseTest> Tests) GetTestsByDisciplineId(int disciplineId, FilterTest filter);

        (int Count, List<DatabaseTest> Tests) GetTestsForStudent(int studentId, FilterTest filter);

        DatabaseTest GetTestForStudentById(int id, int studentId);
    }
}