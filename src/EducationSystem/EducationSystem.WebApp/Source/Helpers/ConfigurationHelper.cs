using AutoMapper;
using EducationSystem.Mapping.Source;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Newtonsoft.Json.Serialization;

namespace EducationSystem.WebApp.Source.Helpers
{
    public class ConfigurationHelper
    {
        public static void ConfigureMapper(IMapperConfigurationExpression expression)
            => MappingConfigurator.Configure(expression);

        public static void ConfigureJson(MvcJsonOptions options)
        {
            options.SerializerSettings.MaxDepth = 1;
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        }

        public static void ConfigureSpaStaticFiles(SpaStaticFilesOptions options) =>
            options.RootPath = "App/build";
    }
}