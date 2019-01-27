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

        public IConfigurationSection GetCorsSection()
        {
            return Configuration.GetSection("Cors");
        }
    }
}