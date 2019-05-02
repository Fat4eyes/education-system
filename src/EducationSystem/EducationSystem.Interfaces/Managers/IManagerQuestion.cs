using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerQuestion
    {
        Task<PagedData<Question>> GetQuestionsAsync(OptionsQuestion options, FilterQuestion filter);

        Task<Question> GetQuestionAsync(int id, OptionsQuestion options);

        Task DeleteQuestionAsync(int id);
        Task UpdateQuestionAsync(int id, Question question);
        Task<int> CreateQuestionAsync(Question question);
    }
}