﻿using FunApp.Common;
using FunApp.Data;
using FunApp.Models.DbModels;
using FunApp.Models.ViewModels.Home;
using FunApp.Services;
using FunApp.Services.Contracts;
using FunApp.Web.ML;
using FunApp.Web.ML.Contracts;
using FunApp.Web.Models.Jokes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FunApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<FunAppContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<FunAppUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequiredUniqueChars = 1;
                })
                .AddEntityFrameworkStores<FunAppContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            AutoMapperConfig.RegisterMappings(
                typeof(IndexViewModel).Assembly,
                typeof(CreateJokeViewModel).Assembly);

            services.AddScoped<IJokeService, JokeService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IJokesCategorizer, JokesCategorizer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
