using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryStudyPlan : IRepositoryReadOnly<DatabaseStudyPlan>
    {
        DatabaseStudyPlan GetStudyPlanByStudentId(int studentId, OptionsStudyPlan options);
    }
}