using System;
using System.Linq.Expressions;

namespace EducationSystem.Interfaces.Specifications
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> ToExpression();

        bool IsSatisfiedBy(TEntity entity);
    }
}