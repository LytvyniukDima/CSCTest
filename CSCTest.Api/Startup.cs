using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CSCTest.Api.Infrastructure;
using CSCTest.DAL.EF;
using Microsoft.EntityFrameworkCore;
using CSCTest.Service.Abstract;
using CSCTest.Service.Concrete;

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
            services.AddScoped<IOrganizationService, OrganizationService>();

            services.AddMvc();
            services.AddSwaggerDocumentation();
            services.AddEFUnitOfWork(Configuration.GetConnectionString("DefaultConnection"));
        
            services.AddDbContext<CSCDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CSCTest.Api")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwaggerDocumentation();
        }
    }
}
