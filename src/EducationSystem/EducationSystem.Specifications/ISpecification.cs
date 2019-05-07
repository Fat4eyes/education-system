using System;
using System.Linq.Expressions;

namespace EducationSystem.Specifications
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> ToExpression();

        bool IsSatisfiedBy(TEntity entity);
    }
}