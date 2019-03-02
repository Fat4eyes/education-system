using AutoMapper;
using EducationSystem.Mapping.Source;
using EducationSystem.Models.Source;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EducationSystem.WebApp.Source.Helpers
{
    public class ConfigurationHelper
    {
        public static void ConfigureMapper(IMapperConfigurationExpression expression)
            => MappingConfigurator.Configure(expression);

        public static void ConfigureSpaStaticFiles(SpaStaticFilesOptions options) =>
            options.RootPath = "App/build";

        public static void ConfigureJson(MvcJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        public static void ConfigureJwtBearer(JwtBearerOptions options, IConfiguration configuration)
        {
            options.RequireHttpsMetadata = false;

            var tokenParameters = configuration
                .GetSection(nameof(TokenParameters))
                .Get<TokenParameters>();

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenParameters.Issuer,
                ValidAudience = tokenParameters.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(tokenParameters.SecretKeyInBytes)
            };
        }
    }
}