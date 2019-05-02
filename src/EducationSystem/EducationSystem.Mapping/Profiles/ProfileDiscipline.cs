using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileDiscipline : Profile
    {
        public ProfileDiscipline()
        {
            CreateMap<DatabaseDiscipline, Discipline>()
                .ForMember(d => d.Tests, o => o.Ignore())
                .ForMember(d => d.Themes, o => o.Ignore());
        }
    }
}