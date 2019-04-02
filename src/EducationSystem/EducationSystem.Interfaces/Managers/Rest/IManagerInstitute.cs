using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerInstitute
    {
        Institute GetInstituteByStudentId(int studentId, OptionsInstitute options);
    }
}