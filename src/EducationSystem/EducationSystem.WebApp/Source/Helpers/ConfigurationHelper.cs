using EducationSystem.Managers.Implementations.Source;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace EducationSystem.WebApp.Source.Helpers
{
    public class ConfigurationHelper
    {
        protected ConfigurationManager ConfigurationManager { get; }

        public ConfigurationHelper(ConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
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