using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CSCTest.Api.Infrastructure
{
    public static class CorsServiceExtensions
    {
        public static IServiceCollection AddCorsSettings(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
            });

            return services;
        }

        public static IApplicationBuilder UseCorsSettings(this IApplicationBuilder app)
        {
            app.UseCors("AllowAllOrigin");
            return app;
        }
    }
}