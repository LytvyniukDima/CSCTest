using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CSCTest.Api.Infrastructure
{
    public static class JwtAthorizationServiceExtensions
    {
        public static IServiceCollection AddJwtAthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Key"])),
                        ValidateIssuerSigningKey = true,
                    };
                });

            return services;
        }
    }
}