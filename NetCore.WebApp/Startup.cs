using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.ViewModels;
using NetCore.WebApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp
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
            services.AddControllersWithViews();

            //Khai báo ConnectionString
            //services.Configure<ConnectionString>(Configuration.GetSection("ConnectionStrings"));

            //Khai báo appSetting
            var appSettingsSection = Configuration.GetSection("AppSetting");
            services.Configure<AppSetting>(appSettingsSection);

            //services.AddSingleton<ArticleAccess>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Tin tuc",
                    pattern: "tin-tuc", new {
                        controller = "Article",
                        action = "NewsPage"
                    });

                endpoints.MapControllerRoute(
                    name: "Video anh",
                    pattern: "video-anh", new
                    {
                        controller = "Article",
                        action = "VideoImage"
                    });
                endpoints.MapControllerRoute(
                    name: "Ung ho truc tuyen",
                    pattern: "ung-ho-truc-tuyen", new
                    {
                        controller = "Article",
                        action = "DonateOnline"
                    });

                endpoints.MapControllerRoute(
                    name: "Chi tiet tin tuc",
                    pattern: "{url}-post{id}", new
                    {
                        controller = "Article",
                        action = "NewsDetail"
                    });
            });
        }
    }
}
