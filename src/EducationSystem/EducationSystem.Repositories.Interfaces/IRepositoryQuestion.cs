using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryQuestion : IRepository<DatabaseQuestion>
    {
        Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestionsAsync(FilterQuestion filter);
        Task<(int Count, List<DatabaseQuestion> Questions)> GetLecturerQuestionsAsync(int lecturerId, FilterQuestion filter);

        Task<DatabaseQuestion> GetLecturerQuestionAsync(int id, int lecturerId);
    }
}