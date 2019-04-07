using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Datas;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTest
    {
        PagedData<Test> GetTests(OptionsTest options, FilterTest filter);
        PagedData<Test> GetTestsByDisciplineId(int disciplineId, OptionsTest options, FilterTest filter);
        PagedData<Test> GetTestsForStudent(int studentId, OptionsTest options, FilterTest filter);

        Test GetTestById(int id, OptionsTest options);
        Test GetTestForStudentById(int id, int studentId, OptionsTest options);

        Task DeleteTestByIdAsync(int id);

        Task<Test> CreateTestAsync(Test test);

        Task<Test> UpdateTestAsync(int id, Test test);
    }
}