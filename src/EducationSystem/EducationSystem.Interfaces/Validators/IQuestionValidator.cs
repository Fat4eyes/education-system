using System.Threading.Tasks;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Validators
{
    public interface IQuestionValidator
    {
        Task<Question> ValidateAsync(Question question);
    }
}