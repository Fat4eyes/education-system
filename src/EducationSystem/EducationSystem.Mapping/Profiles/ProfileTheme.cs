using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileTheme : Profile
    {
        public ProfileTheme()
        {
            CreateMap<Theme, DatabaseTestTheme>()
                .ForMember(d => d.ThemeId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.TestId, o => o.Ignore())
                .ForMember(d => d.Theme, o => o.Ignore())
                .ForMember(d => d.Test, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<DatabaseTheme, Theme>()
                .ForMember(d => d.Questions, o => o.Ignore());

            CreateMap<Theme, DatabaseTheme>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Order, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.ThemeTests, o => o.Ignore())
                .ForMember(d => d.Questions, o => o.Ignore());

            CreateMap<DatabaseTheme, DatabaseTheme>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Order, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.ThemeTests, o => o.Ignore())
                .ForMember(d => d.Questions, o => o.Ignore());
        }
    }
}