using KnoWhere.API.Config;
using KnoWhere.API.Core.MiddleWares;
using KnoWhere.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KnoWhere.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Config/appsettings.json", false, true);
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds services required for using options.
            services.AddOptions();
            // Register the IConfiguration instance which MyOptions binds against.
            services.Configure<Settings>(Configuration);
            // Add MVC service.
            services.AddMvc();
            // Configure DbContext
            services.AddDbContext<KnoWhereContext>(dbOptions => dbOptions.UseSqlServer(Configuration["KnoWhereDbConnectionString"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ElapsedTime>();
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
}