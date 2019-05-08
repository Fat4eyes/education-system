using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverQuestionAnswers : Resolver, IValueResolver<DatabaseQuestion, Question, List<Answer>>
    {
        public ResolverQuestionAnswers(IExecutionContext executionContext)
            : base(executionContext) { }

        public List<Answer> Resolve(DatabaseQuestion source, Question destination, List<Answer> member, ResolutionContext context)
        {
            var mapper = context.Mapper;

            if (CurrentUser.IsAdmin() || CurrentUser.IsLecturer())
                return mapper.Map<List<Answer>>(source.Answers);

            if (CurrentUser.IsNotStudent())
                return null;

            // Ответы на этот тип вопроса представляют собой строки.
            // Все эти строки являются правильным вариантом ответа (просто в разных вариациях).
            if (source.Type == QuestionType.OpenedOneString)
                return null;

            return mapper
                .Map<List<Answer>>(source.Answers)
                .Execute(x => x.IsRight = null);
        }
    }
}