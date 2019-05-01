using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTest
    {
        Task<PagedData<Test>> GetTestsAsync(OptionsTest options, FilterTest filter);
        Task<PagedData<Test>> GetTestsByDisciplineIdAsync(int disciplineId, OptionsTest options, FilterTest filter);

        Task DeleteTestAsync(int id);
        Task<Test> GetTestAsync(int id, OptionsTest options);
        Task<Test> CreateTestAsync(Test test);
        Task<Test> UpdateTestAsync(int id, Test test);
    }
}