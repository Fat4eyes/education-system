using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class ThemesByDisciplineId : Specification<DatabaseTheme>
    {
        private readonly int? _disciplineId;

        public ThemesByDisciplineId(int? disciplineId)
        {
            _disciplineId = disciplineId;
        }

        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            if (_disciplineId.HasValue)
                return x => x.DisciplineId == _disciplineId;

            return x => true;
        }
    }
}