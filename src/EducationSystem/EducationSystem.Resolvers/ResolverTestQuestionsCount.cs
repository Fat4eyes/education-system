using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;

namespace EducationSystem.Resolvers
{
    public class ResolverTestQuestionsCount : IValueResolver<DatabaseTest, Test, int?>
    {
        private readonly IContext _context;
        private readonly IRepository<DatabaseQuestion> _repositoryQuestion;

        public ResolverTestQuestionsCount(IContext context, IRepository<DatabaseQuestion> repositoryQuestion)
        {
            _context = context;
            _repositoryQuestion = repositoryQuestion;
        }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            var user = _context.GetCurrentUser();

            if (user.IsAdmin())
                return _repositoryQuestion.GetCount(new QuestionsByTestId(source.Id));
            
            if (user.IsLecturer())
            {
                var specification =
                    new QuestionsByTestId(source.Id) &
                    new QuestionsByLecturerId(user.Id);

                return _repositoryQuestion.GetCount(specification);
            }

            if (user.IsStudent())
            {
                var specification =
                    new QuestionsByTestId(source.Id) &
                    new QuestionsForStudents() &
                    new QuestionsByStudentId(user.Id);

                return _repositoryQuestion.GetCount(specification);
            }

            return null;
        }
    }
}