using System;
using System.Linq.Expressions;
using EducationSystem.Interfaces.Specifications;

namespace EducationSystem.Implementations.Specifications.Basics
{
    public class OrSpecification<TEntity> : Specification<TEntity>
    {
        private readonly ISpecification<TEntity> _specificationOne;
        private readonly ISpecification<TEntity> _specificationTwo;

        public OrSpecification(ISpecification<TEntity> one, ISpecification<TEntity> two)
        {
            _specificationOne = one;
            _specificationTwo = two;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var expressionOne = _specificationOne.ToExpression();
            var expression = Visitor.GetExpression(expressionOne, _specificationTwo.ToExpression());

            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.OrElse(expressionOne.Body, expression.Body),
                expression.Parameters);
        }
    }
}