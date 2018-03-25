using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CSCTest.Api.Infrastructure;
using CSCTest.DAL.EF;
using Microsoft.EntityFrameworkCore;
using CSCTest.Service.Abstract;
using CSCTest.Service.Concrete;
using AutoMapper;
using CSCTest.Service.Infrastructure;

namespace CSCTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));
            
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IOfferingService, OfferingService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            services.AddJwtAthorization(Configuration.GetSection("AuthOptions"));
            services.AddSwaggerDocumentation();
            services.AddEFUnitOfWork(Configuration.GetConnectionString("AzureConnection"));
            services.AddCorsSettings();

            services.AddMvc();

            services.AddDbContext<CSCDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureConnection"), b => b.MigrationsAssembly("CSCTest.Api")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCorsSettings();
            app.UseAuthentication();
            app.UseHttpStatusCodeExceptionMiddleware();
            app.UseMvc();
            app.UseSwaggerDocumentation();
        }
    }
}
