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

            var sb = new StringBuilder();

            sb.Append($"database = {database.Name};");
            sb.Append($"server = {database.Host};");
            sb.Append($"port = {database.Port};");

            sb.Append($"user id = {database.UserName};");
            sb.Append($"password = {database.UserPassword};");

            services.AddDbContext<EducationSystemDatabaseContext>(x => x.UseMySQL(sb.ToString()));
        }
    }
}