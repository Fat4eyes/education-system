using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerStudyProfile
    {
        StudyProfile GetStudyProfileByUserId(int userId, OptionsStudyProfile options);
    }
}