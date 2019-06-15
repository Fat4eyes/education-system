using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;

namespace EducationSystem.Resolvers
{
    public class ResolverTestPassedQuestionsCount : IValueResolver<DatabaseTest, Test, int?>
    {
        private readonly IContext _context;
        private readonly IRepository<DatabaseQuestion> _repositoryQuestion;

        public ResolverTestPassedQuestionsCount(IContext context, IRepository<DatabaseQuestion> repositoryQuestion)
        {
            _context = context;
            _repositoryQuestion = repositoryQuestion;
        }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            var user = _context.GetCurrentUser();

            if (user.IsNotStudent())
                return null;

            var specification =
                new QuestionsByTestId(source.Id) &
                new QuestionsForStudents() &
                new QuestionsByStudentId(user.Id, true);

            return _repositoryQuestion.GetCount(specification);
        }
    }
}