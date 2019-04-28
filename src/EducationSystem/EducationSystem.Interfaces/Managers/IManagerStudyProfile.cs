using System.Threading.Tasks;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerStudyProfile
    {
        Task<StudyProfile> GetStudyProfileByStudentId(int studentId, OptionsStudyProfile options);
    }
}