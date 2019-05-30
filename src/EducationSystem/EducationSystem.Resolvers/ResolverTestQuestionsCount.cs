using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;

namespace EducationSystem.Resolvers
{
    public class ResolverTestQuestionsCount : Resolver, IValueResolver<DatabaseTest, Test, int?>
    {
        private readonly IRepository<DatabaseQuestion> _repositoryQuestion;

        public ResolverTestQuestionsCount(IContext context, IRepository<DatabaseQuestion> repositoryQuestion) : base(context)
        {
            _repositoryQuestion = repositoryQuestion;
        }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            if (CurrentUser.IsAdmin())
                return _repositoryQuestion.GetCount(new QuestionsByTestId(source.Id));
            
            if (CurrentUser.IsLecturer())
            {
                var specification =
                    new QuestionsByTestId(source.Id) &
                    new QuestionsByLecturerId(CurrentUser.Id);

                return _repositoryQuestion.GetCount(specification);
            }

            if (CurrentUser.IsStudent())
            {
                var specification =
                    new QuestionsByTestId(source.Id) &
                    new QuestionsForStudents() &
                    new QuestionsByStudentId(CurrentUser.Id);

                return _repositoryQuestion.GetCount(specification);
            }

            return null;
        }
    }
}