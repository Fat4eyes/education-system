using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverQuestionHash : IValueResolver<DatabaseQuestion, Question, string>
    {
        private readonly IContext _context;
        private readonly IHashComputer _hashComputer;

        public ResolverQuestionHash(IContext context, IHashComputer hashComputer)
        {
            _context = context;
            _hashComputer = hashComputer;
        }

        public string Resolve(DatabaseQuestion source, Question destination, string member, ResolutionContext context)
        {
            var user = _context.GetCurrentUser();

            if (user.IsNotStudent())
                return null;

            return _hashComputer
                .ComputeForQuestionAsync(source)
                .WaitTask();
        }
    }
}