using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MovieClub.Data;
using System;
using Microsoft.Extensions.Configuration;
using MovieClub.Models;

namespace MovieClub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddDbContext<MvcTextContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MvcTextContext")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MvcTextContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();

            // Configure cookie settings
            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Redirect to login page when unauthorized
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "login",
                pattern: "Login",
                defaults: new { controller = "Login", action = "Login" });

            app.MapControllerRoute(
                name: "Register",
                pattern: "Register",
                defaults: new { controller = "Register", action = "Register" });

            // Seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            app.Run();
        }
    }
}
