using EducationSystem.Managers.Implementations.Source;
using EducationSystem.WebApp.Source.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;

namespace EducationSystem.WebApp.Source.Helpers
{
    public class ConfigurationHelper
    {
        protected ConfigurationManager ConfigurationManager { get; }

        public ConfigurationHelper(ConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
        }

        public static void ConfigureJson(MvcJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        }

        public void ConfigureCors(CorsOptions options)
        {
            var cors = ConfigurationManager
                .GetCorsSection()
                .Get<Cors>();

            options.AddPolicy(cors.Policy, y => ConfigureCorsPolicy(cors, y));
        }

        private static void ConfigureCorsPolicy(Cors cors, CorsPolicyBuilder builder)
        {
            builder
                .WithOrigins(cors.Origins.ToArray())
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    }
}