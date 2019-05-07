using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Themes
{
    public sealed class ThemesByIds : Specification<DatabaseTheme>
    {
        private readonly int[] _ids;

        public ThemesByIds(int[] ids)
        {
            _ids = ids;
        }

        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}