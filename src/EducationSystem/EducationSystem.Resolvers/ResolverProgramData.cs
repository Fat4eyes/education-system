using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverProgramData : Resolver, IValueResolver<DatabaseProgram, Program, List<ProgramData>>
    {
        public ResolverProgramData(IExecutionContext executionContext)
            : base(executionContext) { }

        public List<ProgramData> Resolve(DatabaseProgram source, Program destination, List<ProgramData> member, ResolutionContext context)
        {
            var mapper = context.Mapper;

            if (CurrentUser.IsAdmin() || CurrentUser.IsLecturer())
                return mapper.Map<List<ProgramData>>(source.ProgramDatas);

            return null;
        }
    }
}