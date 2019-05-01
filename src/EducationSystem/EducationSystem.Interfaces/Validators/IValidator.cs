using System.Threading.Tasks;

namespace EducationSystem.Interfaces.Validators
{
    public interface IValidator<in TModel> where TModel : class
    {
        Task ValidateAsync(TModel model);
    }
}