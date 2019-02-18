using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerStudent
    {
        PagedData<Student> GetStudents(OptionsStudent options, Filter filter);
        PagedData<Student> GetStudentsByGroupId(int groupId, OptionsStudent options, Filter filter);

        Student GetStudentById(int id, OptionsStudent options);
    }
}