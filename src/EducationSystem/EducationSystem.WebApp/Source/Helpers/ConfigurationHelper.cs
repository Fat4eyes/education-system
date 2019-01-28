using EducationSystem.WebApp.Source.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;

namespace EducationSystem.WebApp.Source.Helpers
{
    public class ConfigurationHelper
    {
        protected IConfiguration Configuration { get; }

        public ConfigurationHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Cors GetCors() =>
            Configuration
                .GetSection("Cors")
                .Get<Cors>();

        public string[] GetOrigins() =>
            GetCors().Origins.ToArray();

        public string GetPolicy() =>
            GetCors().Policy;

        public IConfigurationSection GetLoggingSection() =>
            Configuration.GetSection("Logging");

        public static void ConfigureJson(MvcJsonOptions options) =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();

        public void ConfigureCors(CorsOptions options) =>
            options.AddPolicy(GetPolicy(), ConfigureCorsPolicy);

        private void ConfigureCorsPolicy(CorsPolicyBuilder builder)
        {
            builder
                .WithOrigins(GetOrigins())
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    }
}