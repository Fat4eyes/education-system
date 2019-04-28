using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerDiscipline
    {
        Task<PagedData<Discipline>> GetDisciplines(OptionsDiscipline options, FilterDiscipline filter);
        Task<PagedData<Discipline>> GetDisciplinesByStudentId(int studentId, OptionsDiscipline options, FilterDiscipline filter);

        Task<Discipline> GetDiscipline(int id, OptionsDiscipline options);
    }
}