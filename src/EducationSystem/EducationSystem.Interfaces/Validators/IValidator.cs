namespace EducationSystem.Interfaces.Validators
{
    public interface IValidator<in TModel> where TModel : class
    {
        void Validate(TModel model);
    }
}