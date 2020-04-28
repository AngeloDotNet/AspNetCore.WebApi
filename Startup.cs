using AEP_WebApi.Services;
using AEP_WebApi.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AEP_WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(options => {
                    options.RegisterValidatorsFromAssemblyContaining<ComuniValidator>();
                });

            var connectionstring = Configuration["ConnectionStrings:ComuniDbConnString"];
            
            services.AddDbContext<ComuniDbContext>(c => c.UseSqlite(connectionstring));
            services.AddScoped<IComuniRepository, ComuniRepository>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(routeBuilder => 
            {
                routeBuilder.MapControllers();
            });
        }
    }
}
