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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Weterynarz.Web.Extensions;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Web.Models.Emails;
using Weterynarz.Domain.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Web.ModelBinders;

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
            services.AddMvc(o => {
                o.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            });

            // enable In-Memory Cache
            services.AddMemoryCache();

            services.AddPaging();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(p => new ApplicationDbContext(p.GetService<DbContextOptions<ApplicationDbContext>>()));

            // Add application repositories
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IAnimalTypesRepository, AnimalTypesRepository>();
            services.AddScoped<IMedicalExaminationTypesRepository, MedicalExaminationTypesRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<ISettingsContentRepository, SettingsContentRepository>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IDoctorGraphicsRepository, DoctorGraphicsRepository>();
            services.AddScoped<IDiseasesRepository, DiseasesRepository>();

            // Add application services.
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            // redirect to another login page
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/Admin/Account/Login";
                options.LogoutPath = "/Admin/Account/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("pl-PL")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pl-PL"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


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
                    template: "UmowWizyte",
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
