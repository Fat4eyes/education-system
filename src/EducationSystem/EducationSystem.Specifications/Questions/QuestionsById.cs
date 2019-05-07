using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Questions
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