using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryStudyPlan : IRepositoryReadOnly<DatabaseStudyPlan>
    {
        Task<DatabaseStudyPlan> GetStudyPlanByStudentId(int studentId);
    }
}