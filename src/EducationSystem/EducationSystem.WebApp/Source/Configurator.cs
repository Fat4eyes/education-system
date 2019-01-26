using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.WebApp.Source
{
    public class Configurator
    {
        // TODO: Вынести в конфигурацию.
        protected const string Policy = "WebAppReact";
        protected const string Origin = nameof(Origin);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(Setup);
        }

        public void Configure(IApplicationBuilder builder, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
                builder.UseDeveloperExceptionPage();

            builder.UseCors(Policy);
            builder.UseMvc();
        }

        private static void Setup(CorsOptions options)
            => options.AddPolicy(Policy, Configure);

        private static void Configure(CorsPolicyBuilder builder)
        {
            builder
                .WithOrigins(Origin)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    }
}