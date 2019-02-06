using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryStudyPlan : IRepositoryReadOnly<DatabaseStudyPlan>
    {
        DatabaseStudyPlan GetStudyPlanByUserId(int userId, OptionsStudyPlan options);
    }
}