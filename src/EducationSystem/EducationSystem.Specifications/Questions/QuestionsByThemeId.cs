using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Questions
{
    public sealed class QuestionsByThemeId : Specification<DatabaseQuestion>
    {
        private readonly int? _themeId;

        public QuestionsByThemeId(int? themeId)
        {
            _themeId = themeId;
        }

        public override Expression<Func<DatabaseQuestion, bool>> ToExpression()
        {
            if (_themeId.HasValue)
                return x => x.ThemeId == _themeId;

            return x => true;
        }
    }
}