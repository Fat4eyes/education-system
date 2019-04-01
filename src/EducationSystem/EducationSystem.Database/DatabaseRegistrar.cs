﻿using System.Text;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.Database
{
    public static class DatabaseRegistrar
    {
        public static void Register(IServiceCollection collection, IConfiguration configuration)
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

            collection.AddDbContext<DatabaseContext>(x => x
                .UseLazyLoadingProxies()
                .UseMySQL(builder.ToString()));
        }
    }
}