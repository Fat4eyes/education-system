using EducationSystem.Managers.Interfaces.Source;
using Microsoft.Extensions.Configuration;

namespace EducationSystem.Managers.Implementations.Source
{
    public class ConfigurationManager : IConfigurationManager
    {
        protected IConfiguration Configuration { get; }

        public ConfigurationManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GetCorsPolicy()
        {
            return Configuration
               .GetSection("Cors")
               .GetSection("Policy").Value ?? string.Empty;
        }

        public string GetCorsOrigin()
        {
            return Configuration
               .GetSection("Cors")
               .GetSection("Origin").Value ?? string.Empty;
        }
    }
}