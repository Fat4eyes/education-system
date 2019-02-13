using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerTest
    {
        PagedData<Test> GetTests(OptionsTest options);
        PagedData<Test> GetTestsByDisciplineId(int disciplineId, OptionsTest options);

        Test GetTestById(int id, OptionsTest options);
    }
}