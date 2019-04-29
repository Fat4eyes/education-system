using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryStudyProfile : IRepositoryReadOnly<DatabaseStudyProfile>
    {
        Task<DatabaseStudyProfile> GetStudyProfileByStudentId(int studentId);
    }
}