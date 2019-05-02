using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceDiscipline
    {
        Task<PagedData<Discipline>> GetDisciplinesAsync(OptionsDiscipline options, FilterDiscipline filter);
        Task<PagedData<Discipline>> GetStudentDisciplinesAsync(int studentId, OptionsDiscipline options, FilterDiscipline filter);
        Task<PagedData<Discipline>> GetLecturerDisciplinesAsync(int lecturerId, OptionsDiscipline options, FilterDiscipline filter);

        Task<Discipline> GetDisciplineAsync(int id, OptionsDiscipline options);
        Task<Discipline> GetStudentDisciplineAsync(int id, int studentId, OptionsDiscipline options);
        Task<Discipline> GetLecturerDisciplineAsync(int id, int lecturerId, OptionsDiscipline options);
    }
}