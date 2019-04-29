using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryInstitute : IRepositoryReadOnly<DatabaseInstitute>
    {
        Task<DatabaseInstitute> GetInstituteByStudentId(int studentId);
    }
}