#pragma warning disable 1591

using System;
using Contracts.DAL.App;
using DAL.App.EF;
using DAL.App.EF.DataInit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Helpers;

namespace WebApp
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                        Configuration.GetConnectionString("PsqlConnection")));

            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            
            services.AddControllersWithViews();
            
            // ================================Swagger============================
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SetupAppData(app, Configuration);

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
            
            //=============================Swagger==============
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                options.RoutePrefix = "";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        
        private static void SetupAppData(IApplicationBuilder app, IConfiguration configuration)
        {
            using var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if (ctx != null)
            {
                if (configuration.GetValue<bool>("AppData:DropDatabase"))
                {
                    Console.Write("Drop database");
                    DataInit.DropDatabase(ctx);
                    Console.WriteLine(" - done");
                }

                if (configuration.GetValue<bool>("AppData:Migrate"))
                {
                    Console.Write("Migrate database");
                    DataInit.MigrateDatabase(ctx);
                    Console.WriteLine(" - done");
                }

                if (configuration.GetValue<bool>("AppData:SeedData"))
                {
                    Console.Write("Seed database");
                    DataInit.SeedAppData(ctx);
                    Console.WriteLine(" - done");
                }
            }
        }
    }
}