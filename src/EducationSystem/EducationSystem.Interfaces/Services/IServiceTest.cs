using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceTest
    {
        Task<PagedData<Test>> GetTestsAsync(OptionsTest options, FilterTest filter);
        Task<PagedData<Test>> GetStudentTestsAsync(int studentId, OptionsTest options, FilterTest filter);
        Task<PagedData<Test>> GetLecturerTestsAsync(int lecturerId, OptionsTest options, FilterTest filter);

        Task<Test> GetTestAsync(int id, OptionsTest options);
        Task<Test> GetStudentTestAsync(int id, int studentId, OptionsTest options);
        Task<Test> GetLecturerTestAsync(int id,int lecturerId, OptionsTest options);

        Task DeleteTestAsync(int id);
        Task UpdateTestAsync(int id, Test test);
        Task<int> CreateTestAsync(Test test);
    }
}