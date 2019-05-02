using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileProgramData : Profile
    {
        public ProfileProgramData()
        {
            CreateMap<DatabaseProgramData, ProgramData>();

            CreateMap<ProgramData, DatabaseProgramData>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.ProgramId, o => o.Ignore());
        }
    }
}