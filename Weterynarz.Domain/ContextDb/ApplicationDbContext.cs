using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Domain.EntitiesDb;

namespace Weterynarz.Domain.ContextDb
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasMany(p => p.Roles).WithOne().HasForeignKey(p => p.UserId).HasPrincipalKey(p => p.Id);

            builder.Entity<Animal>()
                .HasMany(i => i.Visits)
                .WithOne(x => x.Animal);

            builder.Entity<ApplicationUser>()
                .HasMany(i => i.Animals)
                .WithOne(x => x.Owner);
        }

        #region DbSets
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<MedicalExaminationType> MedicalExaminationTypes { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<SettingsContent> SettingsContent { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Graphic> Graphics { get; set; }
        public DbSet<DoctorGraphic> DoctorGraphics { get; set; }
        public DbSet<SummaryVisit> SummaryVisits { get;set }
        #endregion
    }
}
