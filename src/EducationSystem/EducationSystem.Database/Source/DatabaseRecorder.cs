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
            var database = configuration.GetSection("Database");

            var sb = new StringBuilder();

            sb.Append($"database = {database.GetSection("Name").Value};");
            sb.Append($"server = {database.GetSection("Host").Value};");
            sb.Append($"port = {database.GetSection("Port").Value};");

            sb.Append($"user id = {database.GetSection("UserName").Value};");
            sb.Append($"password = {database.GetSection("UserPassword").Value};");

            services.AddDbContext<EducationSystemDatabaseContext>(x => x.UseMySQL(sb.ToString()));
        }
    }
}