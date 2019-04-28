using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTest
    {
        Task<PagedData<Test>> GetTests(OptionsTest options, FilterTest filter);
        Task<PagedData<Test>> GetTestsByDisciplineId(int disciplineId, OptionsTest options, FilterTest filter);

        Task DeleteTest(int id);
        Task<Test> GetTest(int id, OptionsTest options);
        Task<Test> CreateTest(Test test);
        Task<Test> UpdateTest(int id, Test test);
    }
}