using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerDiscipline
    {
        Task<PagedData<Discipline>> GetDisciplinesAsync(FilterDiscipline filter);

        Task<Discipline> GetDisciplineAsync(int id);
    }
}