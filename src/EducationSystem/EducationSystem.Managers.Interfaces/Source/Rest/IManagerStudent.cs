using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerStudent
    {
        PagedData<Student> GetStudents(OptionsStudent options, FilterStudent filter);
        PagedData<Student> GetStudentsByGroupId(int groupId, OptionsStudent options, FilterStudent filter);

        Student GetStudentById(int id, OptionsStudent options);
    }
}