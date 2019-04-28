using System.Threading.Tasks;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerInstitute
    {
        Task<Institute> GetInstituteByStudentId(int studentId, OptionsInstitute options);
    }
}