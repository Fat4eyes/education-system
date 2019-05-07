using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Disciplines
{
    public sealed class DisciplinesById : Specification<DatabaseDiscipline>
    {
        private readonly int _id;

        public DisciplinesById(int id)
        {
            _id = id;
        }

        public override Expression<Func<DatabaseDiscipline, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}