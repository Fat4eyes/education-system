using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Themes
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