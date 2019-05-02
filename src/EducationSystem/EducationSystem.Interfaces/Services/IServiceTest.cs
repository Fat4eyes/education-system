using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceTest
    {
        Task<PagedData<Test>> GetTestsAsync(FilterTest filter);
        Task<PagedData<Test>> GetStudentTestsAsync(int studentId, FilterTest filter);
        Task<PagedData<Test>> GetLecturerTestsAsync(int lecturerId, FilterTest filter);

        Task<Test> GetTestAsync(int id);
        Task<Test> GetStudentTestAsync(int id, int studentId);
        Task<Test> GetLecturerTestAsync(int id, int lecturerId);

        Task DeleteTestAsync(int id);
        Task UpdateTestAsync(int id, Test test);
        Task<int> CreateTestAsync(Test test);
    }
}