using CSCTest.DAL.EF;
using CSCTest.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CSCTest.Api.Infrastructure
{
    public static class UnitOfWorkServiceExtension
    {
        public static IServiceCollection AddEFUnitOfWork(this IServiceCollection services, string connection)
        {
            var builder = new DbContextOptionsBuilder<CSCDbContext>()
                .UseSqlServer(connection);
            services.AddScoped<IUnitOfWork>(provider => new EFUnitOfWork(builder.Options));

            return services;
        }
    }
}