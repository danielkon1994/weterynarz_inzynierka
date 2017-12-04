using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Domain.EntitiesDb;

namespace Weterynarz.Domain.ContextDb
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        #region DbSets
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<MedicalExamination> MedicalExaminations { get; set; }
        public DbSet<MedicalExaminationType> MedicalExaminationTypes { get; set; }
        public DbSet<Visit> Visits { get; set; }
        #endregion
    }
}
