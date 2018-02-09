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

            //builder.Entity<ApplicationUser>()
            //    .HasMany(i => i.Animals)
            //    .WithOne(x => x.Owner);

            //builder.Entity<ApplicationUser>()
            //    .HasMany(x => x.DoctorGraphics)
            //    .WithOne(x => x.Doctor);

            //builder.Entity<ApplicationUser>()
            //    .HasMany(x => x.Visits)
            //    .WithOne(x => x.Doctor);

            //builder.Entity<Animal>()
            //    .HasOne(i => i.AnimalType)
            //    .WithMany(x => x.Animals);

            //builder.Entity<Animal>()
            //    .HasOne(i => i.Owner)
            //    .WithMany(x => x.Animals);

            //builder.Entity<Animal>()
            //    .HasMany(x => x.Visits)
            //    .WithOne(x => x.Animal);

            builder.Entity<AnimalDisease>()
                .HasKey(a => new { a.AnimalId, a.DiseaseId });

            //builder.Entity<AnimalDisease>()
            //    .HasOne(x => x.Animal)
            //    .WithMany(x => x.AnimalDiseases)
            //    .HasForeignKey(x => x.AnimalId);

            //builder.Entity<AnimalDisease>()
            //    .HasOne(x => x.Disease)
            //    .WithMany(x => x.AnimalDiseases)
            //    .HasForeignKey(x => x.DiseaseId);

            builder.Entity<AnimalMedicalExamination>()
                .HasKey(a => new { a.AnimalId, a.MedicalExaminationId });

            //builder.Entity<AnimalMedicalExamination>()
            //    .HasOne(x => x.Animal)
            //    .WithMany(x => x.AnimalMedicalExaminations)
            //    .HasForeignKey(x => x.AnimalId);

            //builder.Entity<AnimalMedicalExamination>()
            //    .HasOne(x => x.MedicalExamination)
            //    .WithMany(x => x.AnimalMedicalExaminations)
            //    .HasForeignKey(x => x.MedicalExaminationId);

            //builder.Entity<DoctorGraphic>()
            //    .HasOne(x => x.Graphic)
            //    .WithOne(x => x.DoctorGraphic)
            //    .HasForeignKey<Graphic>(x => x.DoctorGraphicId);

            //builder.Entity<SummaryVisit>()
            //    .HasOne(x => x.Visit)
            //    .WithOne(x => x.SummaryVisit)
            //    .HasForeignKey<SummaryVisit>(x => x.VisitId);

            //builder.Entity<Visit>()
            //    .HasOne(x => x.Doctor)
            //    .WithMany(x => x.Visits);

            //builder.Entity<Visit>()
            //    .HasOne(x => x.Animal)
            //    .WithMany(x => x.Visits);

            //builder.Entity<Visit>()
            //    .HasOne(x => x.SummaryVisit)
            //    .WithOne(x => x.Visit);
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
        public DbSet<SummaryVisit> SummaryVisits { get; set; }
        public DbSet<AnimalDisease> AnimalDiseases { get; set; }
        public DbSet<AnimalMedicalExamination> AnimalMedicalExaminations { get; set; }
        #endregion
    }
}
