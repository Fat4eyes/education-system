using System.Text;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Database.Source.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.Database.Source
{
    public static class DatabaseRegistrar
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var database = configuration
                .GetSection(nameof(DatabaseParameters))
                .Get<DatabaseParameters>();

            var builder = new StringBuilder()
                .Append($" DATABASE = {database.Name}; ")
                .Append($" SERVER   = {database.Host}; ")
                .Append($" PORT     = {database.Port}; ")
                .Append($" USER ID  = {database.UserName}; ")
                .Append($" PASSWORD = {database.UserPassword}; ");

            services.AddDbContext<DatabaseContext>(x => x
                .UseLazyLoadingProxies()
                .UseMySQL(builder.ToString()));
        }
    }
}