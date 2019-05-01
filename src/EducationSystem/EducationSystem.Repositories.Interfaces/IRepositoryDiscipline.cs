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
        Task<(int Count, List<DatabaseDiscipline> Disciplines)> GetDisciplinesByStudentIdAsync(int studentId, FilterDiscipline filter);
    }
}