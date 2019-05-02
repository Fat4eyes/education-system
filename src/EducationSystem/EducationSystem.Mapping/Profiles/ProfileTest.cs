using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileTest : Profile
    {
        public ProfileTest()
        {
            CreateMap<DatabaseTest, Test>()
                .ForMember(d => d.Attempts, o => o.Ignore())
                .ForMember(d => d.TotalTime, o => o.Ignore())
                .ForMember(d => d.Themes, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive == 1));

            CreateMap<Test, DatabaseTest>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.TestThemes, o => o.MapFrom(s => s.Themes))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive ? 1 : 0));

            CreateMap<DatabaseTest, DatabaseTest>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.TestThemes, o => o.Ignore());
        }
    }
}