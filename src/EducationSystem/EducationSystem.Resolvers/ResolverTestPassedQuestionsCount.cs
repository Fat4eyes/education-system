using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;

namespace EducationSystem.Resolvers
{
    public class ResolverTestPassedQuestionsCount : Resolver, IValueResolver<DatabaseTest, Test, int?>
    {
        public ResolverTestPassedQuestionsCount(IContext context) : base(context) { }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            if (CurrentUser.IsNotStudent())
                return null;

            var specification =
                new QuestionsForStudents() &
                new QuestionsByStudentId(CurrentUser.Id, true);

            return source.TestThemes
                .Select(x => x.Theme)
                .SelectMany(x => x.Questions)
                .Count(specification);
        }
    }
}