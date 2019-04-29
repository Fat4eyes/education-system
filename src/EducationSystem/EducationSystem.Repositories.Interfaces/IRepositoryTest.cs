using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryTest : IRepository<DatabaseTest>
    {
        Task<(int Count, List<DatabaseTest> Tests)> GetTests(FilterTest filter);
        Task<(int Count, List<DatabaseTest> Tests)> GetTestsByDisciplineId(int disciplineId, FilterTest filter);

        Task<(int Count, List<DatabaseTest> Tests)> GetTestsForStudent(int studentId, FilterTest filter);

        Task<DatabaseTest> GetTestForStudentById(int id, int studentId);
    }
}