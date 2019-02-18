using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Mapping.Source
{
    public static class MappingConfigurator
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<DatabaseUser, User>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active == 1))
                .ForMember(d => d.Roles, o => o.Ignore());

            expression.CreateMap<DatabaseUser, Student>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active == 1))
                .ForMember(d => d.Group, o => o.Ignore())
                .ForMember(d => d.Roles, o => o.Ignore())
                .ForMember(d => d.TestResults, o => o.Ignore());

            expression.CreateMap<DatabaseDiscipline, Discipline>()
                .ForMember(d => d.Tests, o => o.Ignore())
                .ForMember(d => d.Themes, o => o.Ignore());

            expression.CreateMap<User, Student>();
            expression.CreateMap<DatabaseRole, Role>();

            expression.CreateMap<DatabaseGroup, Group>()
                .ForMember(d => d.StudyPlan, o => o.Ignore());

            expression.CreateMap<DatabaseStudyPlan, StudyPlan>()
                .ForMember(d => d.StudyProfile, o => o.Ignore());

            expression.CreateMap<DatabaseStudyProfile, StudyProfile>()
                .ForMember(d => d.Institute, o => o.Ignore());

            expression.CreateMap<DatabaseTest, Test>()
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive == 1))
                .ForMember(d => d.Themes, o => o.Ignore());

            expression.CreateMap<DatabaseTheme, Theme>()
                .ForMember(d => d.Questions, o => o.Ignore());

            expression.CreateMap<DatabaseTestResult, TestResult>()
                .ForMember(d => d.Test, o => o.Ignore())
                .ForMember(d => d.GivenAnswers, o => o.Ignore());

            expression.CreateMap<DatabaseGivenAnswer, GivenAnswer>()
                .ForMember(d => d.Question, o => o.Ignore());

            expression.CreateMap<DatabaseQuestion, Question>()
                .ForMember(d => d.Answers, o => o.Ignore())
                .ForMember(d => d.Program, o => o.Ignore());

            expression.CreateMap<DatabaseAnswer, Answer>()
                .ForMember(d => d.IsRight, o => o.MapFrom(s => s.IsRight == 1));

            expression.CreateMap<DatabaseProgram, Program>();

            expression.CreateMap<DatabaseInstitute, Institute>();

            expression.AllowNullCollections = true;
            expression.ForAllMaps(Configure);
        }

        private static void Configure(TypeMap x, IMappingExpression expression)
            => expression.PreserveReferences();
    }
}