using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;
using EducationSystem.Resolvers;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileTest : Profile
    {
        public ProfileTest()
        {
            CreateMap<DatabaseTest, Test>()
                .ForMember(d => d.Themes, o => o.Ignore())
                .ForMember(d => d.ThemesCount, o => o.MapFrom<ResolverTestThemesCount>())
                .ForMember(d => d.QuestionsCount, o => o.MapFrom<ResolverTestQuestionsCount>())
                .ForMember(d => d.PassedThemesCount, o => o.MapFrom<ResolverTestPassedThemesCount>())
                .ForMember(d => d.PassedQuestionsCount, o => o.MapFrom<ResolverTestPassedQuestionsCount>());

            CreateMap<Test, DatabaseTest>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.TestThemes, o => o.MapFrom(s => s.Themes));

            CreateMap<DatabaseTest, DatabaseTest>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.TestThemes, o => o.Ignore());
        }
    }
}