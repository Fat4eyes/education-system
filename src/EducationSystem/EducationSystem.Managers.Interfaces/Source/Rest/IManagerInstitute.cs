using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerInstitute
    {
        Institute GetInstituteByUserId(int userId, OptionsInstitute options);
    }
}