using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Mapping.Source;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace EducationSystem.WebApp.Source.Helpers
{
    public class ConfigurationHelper
    {
        public static void ConfigureMapper(IMapperConfigurationExpression expression)
            => MappingConfigurator.Configure(expression);

        public static void ConfigureJson(MvcJsonOptions options)
        {
            options.SerializerSettings.MaxDepth = 1;
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        }

        public static void ConfigureSpaStaticFiles(SpaStaticFilesOptions options) =>
            options.RootPath = "App/build";

        public static void ConfigureJwtBearer(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenParameters.Issuer,
                ValidAudience = TokenParameters.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(TokenParameters.SecretKeyInBytes)
            };
        }
    }
}