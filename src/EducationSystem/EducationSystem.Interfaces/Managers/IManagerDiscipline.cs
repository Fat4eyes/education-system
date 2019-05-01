using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerDiscipline
    {
        Task<PagedData<Discipline>> GetDisciplinesAsync(OptionsDiscipline options, FilterDiscipline filter);
        Task<PagedData<Discipline>> GetDisciplinesByStudentIdAsync(int studentId, OptionsDiscipline options, FilterDiscipline filter);

        Task<Discipline> GetDisciplineAsync(int id, OptionsDiscipline options);
    }
}