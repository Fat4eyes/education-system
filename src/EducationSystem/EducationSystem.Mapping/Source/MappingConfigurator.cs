using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source;

namespace EducationSystem.Mapping.Source
{
    /// <summary>
    /// Конфигуратор преобразования типов.
    /// </summary>
    public static class MappingConfigurator
    {
        /// <summary>
        /// Конфигурирует правила преобразования типов.
        /// </summary>
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<DatabaseUser, User>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active == 1));

            expression.CreateMap<DatabaseGroup, Group>()
                .ForMember(d => d.IsFullTime, o => o.MapFrom(s => s.IsFullTime == 1));

            expression.CreateMap<DatabaseStudyPlan, StudyPlan>();
        }
    }
}