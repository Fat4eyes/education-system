using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryInstitute : IRepositoryReadOnly<DatabaseInstitute>
    {
        DatabaseInstitute GetInstituteByUserId(int userId, OptionsInstitute options);
    }
}