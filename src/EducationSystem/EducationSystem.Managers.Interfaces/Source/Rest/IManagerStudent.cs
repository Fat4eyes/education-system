using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerStudent
    {
        PagedData<Student> GetStudents(OptionsStudent options);
        PagedData<Student> GetStudentsByGroupId(int groupId, OptionsStudent options);

        Student GetStudentById(int id, OptionsStudent options);
    }
}