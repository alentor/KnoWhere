using System;
using System.Diagnostics;
using KnoWhere.API.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                await next.Invoke();
                stopwatch.Stop();
                Debug.WriteLine($"<!-- {stopwatch.ElapsedMilliseconds} ms -->");
            });
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
}