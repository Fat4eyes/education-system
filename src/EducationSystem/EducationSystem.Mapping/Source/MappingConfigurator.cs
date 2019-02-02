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
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.Roles.Select(x => x.Role)));

            expression.CreateMap<DatabaseGroup, Group>()
                .ForMember(d => d.IsFullTime, o => o.MapFrom(s => s.IsFullTime == 1));

            expression.CreateMap<DatabaseStudyPlan, StudyPlan>();
            expression.CreateMap<DatabaseStudyProfile, StudyProfile>();
            expression.CreateMap<DatabaseInstitute, Institute>();
            expression.CreateMap<DatabaseRole, Role>();

            expression.CreateMap<User, UserShort>();
            expression.CreateMap<Role, RoleShort>();

            expression.ForAllMaps(Configure);
        }

        private static void Configure(TypeMap x, IMappingExpression expression)
            => expression.PreserveReferences();
    }
}