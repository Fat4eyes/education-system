using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class QuestionsByIds : Specification<DatabaseQuestion>
    {
        public readonly int[] _ids;

        public QuestionsByIds(int[] ids)
        {
            _ids = ids;
        }

        public override Expression<Func<DatabaseQuestion, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}