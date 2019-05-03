using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Implementations.Specifications.Extensions;
using EducationSystem.Interfaces.Specifications;

namespace EducationSystem.Implementations.Specifications.Basics
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression();

        protected Specification()
        {
            LazyPredicate = new Lazy<Func<TEntity, bool>>(() => ToExpression().Compile());
        }

        public bool IsSatisfiedBy(TEntity entity) => LazyPredicate.Value(entity);

        public static implicit operator Func<TEntity, bool>
            (Specification<TEntity> specification) => specification.IsSatisfiedBy;

        public static implicit operator Expression<Func<TEntity, bool>>
            (Specification<TEntity> specification) => specification.ToExpression();

        public static Specification<TEntity> operator |
            (Specification<TEntity> one, Specification<TEntity> two) => one.Or(two);

        public static Specification<TEntity> operator &
            (Specification<TEntity> one, Specification<TEntity> two) => one.And(two);

        public static Specification<TEntity> operator !
            (Specification<TEntity> current) => current.Not();

        protected Lazy<Func<TEntity, bool>> LazyPredicate { get; set; }

        protected class Visitor : ExpressionVisitor
        {
            private readonly ParameterExpression _one;
            private readonly ParameterExpression _two;

            public Visitor(ParameterExpression one, ParameterExpression two)
            {
                _one = one;
                _two = two;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return !ReferenceEquals(node, _two)
                    ? base.VisitParameter(node)
                    : _one;
            }

            public static Expression<Func<TEntity, bool>> GetExpression(
                Expression<Func<TEntity, bool>> one,
                Expression<Func<TEntity, bool>> two)
            {
                return Create(one.Parameters.Single(), two.Parameters.Single())
                    .Visit(two) as Expression<Func<TEntity, bool>>;
            }

            public static Visitor Create(ParameterExpression one, ParameterExpression two)
                => new Visitor(one, two);
        }
    }
}