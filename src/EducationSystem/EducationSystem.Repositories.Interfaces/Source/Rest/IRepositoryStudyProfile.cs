using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryStudyProfile : IRepositoryReadOnly<DatabaseStudyProfile>
    {
        DatabaseStudyProfile GetStudyProfileByStudentId(int studentId);
    }
}