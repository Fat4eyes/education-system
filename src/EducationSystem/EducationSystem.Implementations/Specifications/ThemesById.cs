using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class ThemesById : Specification<DatabaseTheme>
    {
        private readonly int _id;

        public ThemesById(int id)
        {
            _id = id;
        }

        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}