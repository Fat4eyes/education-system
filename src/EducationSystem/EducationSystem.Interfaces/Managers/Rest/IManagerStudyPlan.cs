using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerStudyPlan
    {
        StudyPlan GetStudyPlanByStudentId(int studentId, OptionsStudyPlan options);
    }
}