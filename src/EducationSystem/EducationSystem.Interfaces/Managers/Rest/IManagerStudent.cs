using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerStudent
    {
        PagedData<Student> GetStudents(OptionsStudent options, FilterStudent filter);
        PagedData<Student> GetStudentsByGroupId(int groupId, OptionsStudent options, FilterStudent filter);

        Student GetStudentById(int id, OptionsStudent options);
    }
}