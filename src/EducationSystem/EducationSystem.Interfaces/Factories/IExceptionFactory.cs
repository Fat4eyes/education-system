using System;

namespace EducationSystem.Interfaces.Factories
{
    public interface IExceptionFactory
    {
        Exception NotFound<TModel>(int id) where TModel : class;
    }
}