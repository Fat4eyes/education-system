using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class QuestionsById : Specification<DatabaseQuestion>
    {
        public readonly int _id;

        public QuestionsById(int id)
        {
            _id = id;
        }

        public override Expression<Func<DatabaseQuestion, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}