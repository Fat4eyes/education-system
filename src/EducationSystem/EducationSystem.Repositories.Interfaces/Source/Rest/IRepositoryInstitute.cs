using EducationSystem.Database.Models.Source;
namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryInstitute : IRepositoryReadOnly<DatabaseInstitute>
    {
        DatabaseInstitute GetInstituteByStudentId(int studentId);
    }
}