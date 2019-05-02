using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Repositories.Basics;
using EducationSystem.Models.Filters;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryTest : IRepository<DatabaseTest>
    {
        Task<(int Count, List<DatabaseTest> Tests)> GetTestsAsync(FilterTest filter);
        Task<(int Count, List<DatabaseTest> Tests)> GetStudentTestsAsync(int studentId, FilterTest filter);
        Task<(int Count, List<DatabaseTest> Tests)> GetLecturerTestsAsync(int lecturerId, FilterTest filter);

        Task<DatabaseTest> GetStudentTestAsync(int id, int studentId);
        Task<DatabaseTest> GetLecturerTestAsync(int id, int lecturerId);
    }
}