using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Repositories.Basics;
using EducationSystem.Models.Filters;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryQuestion : IRepository<DatabaseQuestion>
    {
        Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestionsAsync(FilterQuestion filter);
        Task<(int Count, List<DatabaseQuestion> Questions)> GetLecturerQuestionsAsync(int lecturerId, FilterQuestion filter);

        Task<DatabaseQuestion> GetLecturerQuestionAsync(int id, int lecturerId);
    }
}