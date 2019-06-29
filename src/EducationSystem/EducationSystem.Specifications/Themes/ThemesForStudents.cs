using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Themes
{
    public sealed class ThemesForStudents : Specification<DatabaseTheme>
    {
        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            var types = QuestionTypeHelper.GetSupportedTypes();

            return x => x.Questions.Any(y => types.Contains(y.Type)) &&
                        x.ThemeTests.Any(y => y.Test.IsActive);
        }
    }
}