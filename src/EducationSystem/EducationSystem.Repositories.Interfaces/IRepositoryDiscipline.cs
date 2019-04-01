using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryDiscipline : IRepositoryReadOnly<DatabaseDiscipline>
    {
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(FilterDiscipline filter);
        (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplinesForStudent(int studentId, FilterDiscipline filter);
    }
}