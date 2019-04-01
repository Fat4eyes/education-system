using EducationSystem.Database.Models.Source;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryStudyPlan : IRepositoryReadOnly<DatabaseStudyPlan>
    {
        DatabaseStudyPlan GetStudyPlanByStudentId(int studentId);
    }
}