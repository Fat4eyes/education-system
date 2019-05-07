using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Themes
{
    public sealed class ThemesForStudents : Specification<DatabaseTheme>
    {
        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            return x => x.Questions.Any(y => QuestionTypes.Supported.Contains(y.Type)) &&
                        x.ThemeTests
                            .Select(y => y.Test)
                            .Any(y => y.IsActive == 1);
        }
    }
}