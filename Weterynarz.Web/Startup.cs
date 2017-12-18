using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weterynarz.Web.Models;
using Weterynarz.Web.Services;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.Repositories.Implementations;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Weterynarz.Web.Extensions;
using ReflectionIT.Mvc.Paging;

namespace Weterynarz.Web
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
            services.AddMvc();

            services.AddPaging();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application repositories
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IAnimalTypesRepository, AnimalTypesRepository>();
            services.AddScoped<IMedicalExaminationTypesRepository, MedicalExaminationTypesRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();

            // Add application services.
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IAnimalTypesService, AnimalTypesService>();
            services.AddScoped<IMedicalExaminationTypesService, MedicalExaminationTypesService>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddTransient<IEmailSender, EmailSender>();

            // redirect to another login page
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Admin/Account/Login";
                options.LogoutPath = "/Admin/Account/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Create super user !
            //CreateSuperUser.Create(serviceProvider).Wait();

            app.UseStaticFiles();

            app.UseAuthentication();

            #region Routes
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Admin",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "visit",
                    template: "umow_wizyte",
                    defaults: new {controller = "Visit", action = "MakeVisit"}
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion
        }
    }
}
