using System.Threading.Tasks;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerStudyPlan
    {
        Task<StudyPlan> GetStudyPlanByStudentId(int studentId, OptionsStudyPlan options);
    }
}