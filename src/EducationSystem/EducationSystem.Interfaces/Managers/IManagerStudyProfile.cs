using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerStudyProfile
    {
        StudyProfile GetStudyProfileByStudentId(int studentId, OptionsStudyProfile options);
    }
}