using EducationSystem.Managers.Implementations.Source;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
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
            options.AddPolicy(ConfigurationManager.GetCorsPolicy(), ConfigureCorsPolicy);
        }

        private void ConfigureCorsPolicy(CorsPolicyBuilder builder)
        {
            builder
                .WithOrigins(ConfigurationManager.GetCorsOrigin())
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    }
}