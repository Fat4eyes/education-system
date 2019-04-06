using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerStudent
    {
        PagedData<Student> GetStudents(OptionsStudent options, FilterStudent filter);
        PagedData<Student> GetStudentsByGroupId(int groupId, OptionsStudent options, FilterStudent filter);

        Student GetStudentById(int id, OptionsStudent options);
    }
}