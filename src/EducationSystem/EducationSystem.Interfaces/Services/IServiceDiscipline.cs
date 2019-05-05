using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceDiscipline
    {
        Task<PagedData<Discipline>> GetDisciplinesAsync(FilterDiscipline filter);

        Task<Discipline> GetDisciplineAsync(int id);
    }
}