using System.Threading.Tasks;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceTestProcess
    {
        Task<Question> GetQuestionAsync(int id);

        Task<Question> ProcessQuestionAsync(int id, Question question);
    }
}