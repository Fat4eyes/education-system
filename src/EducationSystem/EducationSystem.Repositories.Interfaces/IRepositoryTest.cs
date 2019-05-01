using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryTest : IRepository<DatabaseTest>
    {
        Task<(int Count, List<DatabaseTest> Tests)> GetTestsAsync(FilterTest filter);
        Task<(int Count, List<DatabaseTest> Tests)> GetTestsByDisciplineIdAsync(int disciplineId, FilterTest filter);

        Task<(int Count, List<DatabaseTest> Tests)> GetTestsByStudentId(int studentId, FilterTest filter);

        Task<DatabaseTest> GetTestForStudentByIdAsync(int id, int studentId);
    }
}