using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;
using EducationSystem.Resolvers;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileQuestion : Profile
    {
        public ProfileQuestion()
        {
            CreateMap<DatabaseQuestion, Question>()
                .ForMember(d => d.Answers, o => o.MapFrom<ResolverQuestionAnswers>())
                .ForMember(d => d.Hash, o => o.MapFrom<ResolverQuestionHash>())
                .ForMember(d => d.TestId, o => o.Ignore())
                .ForMember(d => d.Right, o => o.Ignore());

            CreateMap<Question, DatabaseQuestion>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Order, o => o.Ignore())
                .ForMember(d => d.Theme, o => o.Ignore())
                .ForMember(d => d.Image, o => o.Ignore())
                .ForMember(d => d.Answers, o => o.Ignore())
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.Material, o => o.Ignore())
                .ForMember(d => d.QuestionStudents, o => o.Ignore())
                .ForMember(d => d.ImageId, o => o.MapFrom(d => d.Image.Id))
                .ForMember(d => d.MaterialId, o => o.MapFrom(d => d.Material.Id));

            CreateMap<DatabaseQuestion, DatabaseQuestion>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Order, o => o.Ignore())
                .ForMember(d => d.Theme, o => o.Ignore())
                .ForMember(d => d.Image, o => o.Ignore())
                .ForMember(d => d.Answers, o => o.Ignore())
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.Material, o => o.Ignore())
                .ForMember(d => d.QuestionStudents, o => o.Ignore());
        }
    }
}