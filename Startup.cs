using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using C_Sharp_Project.Models;
using Microsoft.AspNetCore.Http;

namespace C_Sharp_Project
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
            services.AddSession();
            services.AddDbContext<VolunteerContext>(options => options.UseMySql(Configuration["DBInfo:ConnectionString"]));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=User}/{action=Login}");
                routes.MapRoute(
                    name: "home",
                    template: "{controller=Home}/{action=Home}/{id?}");
                routes.MapRoute(
                    name: "create",
                    template: "{controller=Create}/{action=Home}/{id?}");
            });
        }
    }
}
