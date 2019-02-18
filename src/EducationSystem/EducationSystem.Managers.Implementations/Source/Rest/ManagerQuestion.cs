using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerQuestion : Manager<ManagerQuestion>, IManagerQuestion
    {
        protected IRepositoryQuestion RepositoryQuestion { get; }

        public ManagerQuestion(
            IMapper mapper,
            ILogger<ManagerQuestion> logger,
            IRepositoryQuestion repositoryQuestion)
            : base(mapper, logger)
        {
            RepositoryQuestion = repositoryQuestion;
        }

        public PagedData<Question> GetQuestionsByThemeId(int themeId, OptionsQuestion options, Filter filter)
        {
            var (count, questions) = RepositoryQuestion.GetQuestionsByThemeId(themeId, filter);

            return new PagedData<Question>(questions.Select(x => Map(x, options)).ToList(), count);
        }

        private Question Map(DatabaseQuestion question, OptionsQuestion options)
        {
            return Mapper.Map<DatabaseQuestion, Question>(question, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithAnswers)
                        d.Answers = Mapper.Map<List<Answer>>(s.Answers);
                });
            });
        }
    }
}