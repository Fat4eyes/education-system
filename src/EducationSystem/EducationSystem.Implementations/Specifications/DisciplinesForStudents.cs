using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class DisciplinesForStudents : Specification<DatabaseDiscipline>
    {
        public override Expression<Func<DatabaseDiscipline, bool>> ToExpression()
        {
            return x => x.Tests.Any(y => y.IsActive == 1 && y.TestThemes.Any(z => z.Theme.Questions.Any()));
        }
    }
}