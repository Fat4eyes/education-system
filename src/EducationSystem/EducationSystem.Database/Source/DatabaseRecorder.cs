using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.Database.Source
{
    public static class DatabaseRecorder
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var database = configuration
                .GetSection(nameof(Database))
                .Get<Database>();

            var builder = new StringBuilder();

            builder.Append($" database    = {database.Name}; ");
            builder.Append($" server      = {database.Host}; ");
            builder.Append($" port        = {database.Port}; ");
            builder.Append($" user id     = {database.UserName}; ");
            builder.Append($" password    = {database.UserPassword}; ");

            services.AddDbContext<EducationSystemDatabaseContext>(x => x
                .UseLazyLoadingProxies()
                .UseMySQL(builder.ToString()));
        }

        private class Database
        {
            public string Name { get; set; }
            public string Host { get; set; }
            public string Port { get; set; }
            public string UserName { get; set; }
            public string UserPassword { get; set; }
        }
    }
}