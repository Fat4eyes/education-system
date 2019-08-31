using System.Text;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace EducationSystem.Database
{
    public static class DatabaseRegistrar
    {
        public static void Register(IServiceCollection collection, IConfiguration configuration)
        {
            var parameters = configuration
                .GetSection(nameof(DatabaseParameters))
                .Get<DatabaseParameters>();

            collection.AddDbContextPool<DatabaseContext>(x => Configure(parameters, x));
        }

        private static void Configure(DatabaseParameters parameters, DbContextOptionsBuilder builder)
        {
            var connection = new StringBuilder()
                .Append($" DATABASE = {parameters.Name}; ")
                .Append($" SERVER   = {parameters.Host}; ")
                .Append($" PORT     = {parameters.Port}; ")
                .Append($" USER ID  = {parameters.UserName}; ")
                .Append($" PASSWORD = {parameters.UserPassword}; ")
                .ToString();

            builder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(x => x.Throw(RelationalEventId.QueryClientEvaluationWarning))
                .UseMySql(connection, x => Configure(parameters, x));
        }

        private static void Configure(DatabaseParameters parameters, MySqlDbContextOptionsBuilder builder)
        {
            builder
                .UnicodeCharSet(CharSet.Utf8mb4)
                .ServerVersion(parameters.Version);
        }
    }
}