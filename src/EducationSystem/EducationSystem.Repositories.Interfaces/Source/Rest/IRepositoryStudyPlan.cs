using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryStudyPlan : IRepositoryReadOnly<DatabaseStudyPlan>
    {
        DatabaseStudyPlan GetStudyPlanByStudentId(int studentId);
    }
}