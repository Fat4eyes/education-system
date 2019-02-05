using System.Linq;
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
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(x => x.Role)));

            expression.CreateMap<DatabaseUser, UserWithGroup>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active == 1))
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(x => x.Role)))
                .ForMember(d => d.Group, o => o.MapFrom(s => s.StudentGroup.Group));

            expression.CreateMap<DatabaseUser, UserWithGroupAndStudyPlan>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active == 1))
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(x => x.Role)))
                .ForMember(d => d.Group, o => o.MapFrom(s => s.StudentGroup.Group));

            expression.CreateMap<DatabaseUser, UserWithTestResults>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active == 1))
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(x => x.Role)))
                .ForMember(d => d.Group, o => o.MapFrom(s => s.StudentGroup.Group));

            expression.CreateMap<DatabaseGroup, Group>()
                .ForMember(d => d.IsFullTime, o => o.MapFrom(s => s.IsFullTime == 1));

            expression.CreateMap<DatabaseGroup, GroupWithStudyPlan>()
                .ForMember(d => d.IsFullTime, o => o.MapFrom(s => s.IsFullTime == 1));

            expression.CreateMap<DatabaseStudyPlan, StudyPlan>();
            expression.CreateMap<DatabaseStudyProfile, StudyProfile>();
            expression.CreateMap<DatabaseInstitute, Institute>();
            expression.CreateMap<DatabaseTest, Test>();
            expression.CreateMap<DatabaseTheme, Theme>();
            expression.CreateMap<DatabaseTestResult, TestResult>();

            expression.ForAllMaps(Configure);
        }

        private static void Configure(TypeMap x, IMappingExpression expression)
            => expression.PreserveReferences();
    }
}