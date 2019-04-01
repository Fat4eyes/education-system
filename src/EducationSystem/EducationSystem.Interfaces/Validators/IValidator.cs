namespace EducationSystem.Interfaces.Validators
{
    public interface IValidator<in TModel> where TModel : class
    {
        void Check(TModel model);
    }
}