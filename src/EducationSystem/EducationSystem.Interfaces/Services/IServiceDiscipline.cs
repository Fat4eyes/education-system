using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceDiscipline
    {
        Task<PagedData<Discipline>> GetDisciplinesAsync(FilterDiscipline filter);
        Task<PagedData<Discipline>> GetStudentDisciplinesAsync(int studentId, FilterDiscipline filter);
        Task<PagedData<Discipline>> GetLecturerDisciplinesAsync(int lecturerId, FilterDiscipline filter);

        Task<Discipline> GetDisciplineAsync(int id);
        Task<Discipline> GetStudentDisciplineAsync(int id, int studentId);
        Task<Discipline> GetLecturerDisciplineAsync(int id, int lecturerId);
    }
}