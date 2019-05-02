using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTest
    {
        Task<PagedData<Test>> GetTestsAsync(FilterTest filter);

        Task<Test> GetTestAsync(int id);

        Task DeleteTestAsync(int id);
        Task UpdateTestAsync(int id, Test test);
        Task<int> CreateTestAsync(Test test);
    }
}