using AutoMapper;
using EducationSystem.Mapping.Profiles;

namespace EducationSystem.Mapping
{
    public static class MappingConfigurator
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.AddProfile<ProfileUser>();
            expression.AddProfile<ProfileDiscipline>();
            expression.AddProfile<ProfileGroup>();
            expression.AddProfile<ProfileTest>();
            expression.AddProfile<ProfileTheme>();
            expression.AddProfile<ProfileQuestion>();
            expression.AddProfile<ProfileAnswer>();
            expression.AddProfile<ProfileProgram>();
            expression.AddProfile<ProfileProgramData>();
            expression.AddProfile<ProfileRole>();
            expression.AddProfile<ProfileFile>();

            expression.AllowNullCollections = true;
            expression.ForAllMaps(Configure);
        }

        private static void Configure(TypeMap x, IMappingExpression expression)
            => expression.PreserveReferences();
    }
}