using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
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

        public PagedData<Question> GetQuestionsByThemeId(int themeId, OptionsQuestion options)
        {
            var (count, questions) = RepositoryQuestion.GetQuestionsByThemeId(themeId, options);

            return new PagedData<Question>(Mapper.Map<List<Question>>(questions), count);
        }
    }
}