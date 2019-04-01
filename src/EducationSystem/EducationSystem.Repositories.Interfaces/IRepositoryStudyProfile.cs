using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryStudyProfile : IRepositoryReadOnly<DatabaseStudyProfile>
    {
        DatabaseStudyProfile GetStudyProfileByStudentId(int studentId);
    }
}