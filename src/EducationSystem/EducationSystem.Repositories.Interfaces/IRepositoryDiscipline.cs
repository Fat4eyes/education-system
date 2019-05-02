using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryDiscipline : IRepositoryReadOnly<DatabaseDiscipline>
    {
        Task<(int Count, List<DatabaseDiscipline> Disciplines)> GetDisciplinesAsync(FilterDiscipline filter);
        Task<(int Count, List<DatabaseDiscipline> Disciplines)> GetStudentDisciplinesAsync(int studentId, FilterDiscipline filter);
        Task<(int Count, List<DatabaseDiscipline> Disciplines)> GetLecturerDisciplinesAsync(int lecturerId, FilterDiscipline filter);

        Task<DatabaseDiscipline> GetStudentDisciplineAsync(int id, int studentId);
        Task<DatabaseDiscipline> GetLecturerDisciplineAsync(int id, int lecturerId);
    }
}