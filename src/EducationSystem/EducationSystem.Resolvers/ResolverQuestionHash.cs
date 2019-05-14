using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverQuestionHash : Resolver, IValueResolver<DatabaseQuestion, Question, string>
    {
        private readonly IHashComputer _hashComputer;

        public ResolverQuestionHash(IContext context, IHashComputer hashComputer) : base(context)
        {
            _hashComputer = hashComputer;
        }

        public string Resolve(DatabaseQuestion source, Question destination, string member, ResolutionContext context)
        {
            if (CurrentUser.IsNotStudent())
                return null;

            return _hashComputer
                .ComputeForQuestionAsync(source)
                .WaitTask();
        }
    }
}