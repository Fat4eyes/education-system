using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;

namespace EducationSystem.Resolvers
{
    public class ResolverTestQuestionsCount : Resolver, IValueResolver<DatabaseTest, Test, int?>
    {
        public ResolverTestQuestionsCount(IExecutionContext executionContext)
            : base(executionContext) { }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            if (CurrentUser.IsAdmin())
            {
                return source.TestThemes
                    .Select(x => x.Theme)
                    .SelectMany(x => x.Questions)
                    .Count();
            }
            
            if (CurrentUser.IsLecturer())
            {
                return source.TestThemes
                    .Select(x => x.Theme)
                    .SelectMany(x => x.Questions)
                    .Count(new QuestionsByLecturerId(CurrentUser.Id));
            }

            if (CurrentUser.IsStudent())
            {
                var specification =
                    new QuestionsForStudents() &
                    new QuestionsByStudentId(CurrentUser.Id);

                return source.TestThemes
                    .Select(x => x.Theme)
                    .SelectMany(x => x.Questions)
                    .Count(specification);
            }

            return null;
        }
    }
}