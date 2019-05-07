using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Datas;
using EducationSystem.Resolvers;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileTestData : Profile
    {
        public ProfileTestData()
        {
            CreateMap<DatabaseTest, TestData>()
                .ForMember(d => d.Test, o => o.MapFrom(s => s))
                .ForMember(d => d.ThemesCount, o => o.MapFrom<ResolverTestDataThemes>())
                .ForMember(d => d.QuestionsCount, o => o.MapFrom<ResolverTestDataQuestions>());
        }
    }
}