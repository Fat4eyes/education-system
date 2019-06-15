using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverQuestionAnswers : IValueResolver<DatabaseQuestion, Question, List<Answer>>
    {
        private readonly IContext _context;

        public ResolverQuestionAnswers(IContext context)
        {
            _context = context;
        }

        public List<Answer> Resolve(DatabaseQuestion source, Question destination, List<Answer> member, ResolutionContext context)
        {
            var mapper = context.Mapper;

            var user = _context.GetCurrentUser();

            if (user.IsAdmin() || user.IsLecturer())
                return mapper.Map<List<Answer>>(source.Answers);

            if (user.IsNotStudent())
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