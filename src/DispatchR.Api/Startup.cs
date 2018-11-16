using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.AspNetCore;
using DispatchR.Data.Models;
using DispatchR.Hubs;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace DispatchR.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwagger();

            services.AddDbContext<PlacesContext>((options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DispatchRDatabase"),
                    (sqlOptions) =>
                    {
                        sqlOptions.UseNetTopologySuite();
                    });
            });

            services.AddHealthChecks()
                    .AddDbContextCheck<PlacesContext>();

            services.AddSignalR((options) => {
                // no customization needed, yet
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/ready");
            
            app.UseMvc();
            app.UseSwaggerUi3WithApiExplorer();
            
            app.UseSignalR((routes) => {
                routes.MapHub<DispatchRHub>("/dispatchr");
            });
        }
    }
}
