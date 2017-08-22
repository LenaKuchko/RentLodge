using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentLodge.Data;
using RentLodge.Models;
using RentLodge.Services;
using Microsoft.AspNetCore.Identity;

namespace RentLodge
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseSession();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

             // DatabaseInitialize(app.ApplicationServices).Wait();
        }

        //public async Task DatabaseInitialize(IServiceProvider serviceProvider)
        //{
        //    UserManager<ApplicationUser> userManager =
        //        serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //    RoleManager<IdentityRole> roleManager =
        //        serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    string adminEmail = "admin@gmail.com";
        //    string password = "Admin!1";
        //    int countryId = 1;
        //    if (await roleManager.FindByNameAsync("admin") == null)
        //    {
        //        await roleManager.CreateAsync(new IdentityRole("admin"));
        //    }
        //    if (await roleManager.FindByNameAsync("user") == null)
        //    {
        //        await roleManager.CreateAsync(new IdentityRole("user"));
        //    }
        //    if (await userManager.FindByNameAsync(adminEmail) == null)
        //    {
        //        ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail, CountryId = countryId};
        //        IdentityResult result = await userManager.CreateAsync(admin, password);
        //        if (result.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(admin, "admin");
        //        }
        //    }

        //}
    }
}
