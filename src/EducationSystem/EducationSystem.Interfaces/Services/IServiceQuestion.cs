using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceQuestion
    {
        Task<PagedData<Question>> GetQuestionsAsync(FilterQuestion filter);
        Task<PagedData<Question>> GetLecturerQuestionsAsync(int lecturerId, FilterQuestion filter);

        Task<Question> GetQuestionAsync(int id);
        Task<Question> GetLecturerQuestionAsync(int id, int lecturerId);

        Task DeleteQuestionAsync(int id);
        Task UpdateQuestionAsync(int id, Question question);
        Task<int> CreateQuestionAsync(Question question);
    }
}