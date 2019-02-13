using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerStudyPlan
    {
        StudyPlan GetStudyPlanByStudentId(int studentId, OptionsStudyPlan options);
    }
}