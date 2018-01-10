﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Weterynarz.Domain.ContextDb;

namespace Weterynarz.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int?>("AnimalTypeId");

                    b.Property<DateTime>("BirthDate");

                    b.Property<int?>("ClientId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModificationDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("AnimalTypeId");

                    b.HasIndex("ClientId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.AnimalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModificationDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AnimalTypes");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Active");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("HouseNumber");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModificationDate");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime?>("ModificationDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.MedicalExamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int>("AnimalId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("DoctorId");

                    b.Property<DateTime>("ExaminationDate");

                    b.Property<int>("MedicalExaminationTypeId");

                    b.Property<DateTime?>("ModificationDate");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("MedicalExaminationTypeId");

                    b.ToTable("MedicalExaminations");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.MedicalExaminationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModificationDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("MedicalExaminationTypes");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.SettingsContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Group");

                    b.Property<DateTime?>("ModificationDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("SettingsContent");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int>("AnimalId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("DoctorId")
                        .IsRequired();

                    b.Property<DateTime?>("ModificationDate");

                    b.Property<DateTime>("VisitDate");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Weterynarz.Domain.EntitiesDb.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Weterynarz.Domain.EntitiesDb.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Weterynarz.Domain.EntitiesDb.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Weterynarz.Domain.EntitiesDb.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.Animal", b =>
                {
                    b.HasOne("Weterynarz.Domain.EntitiesDb.AnimalType", "AnimalType")
                        .WithMany()
                        .HasForeignKey("AnimalTypeId");

                    b.HasOne("Weterynarz.Domain.EntitiesDb.Client")
                        .WithMany("Animals")
                        .HasForeignKey("ClientId");

                    b.HasOne("Weterynarz.Domain.EntitiesDb.ApplicationUser", "Owner")
                        .WithMany("Animals")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.MedicalExamination", b =>
                {
                    b.HasOne("Weterynarz.Domain.EntitiesDb.Animal", "Animal")
                        .WithMany("MedicalExaminations")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Weterynarz.Domain.EntitiesDb.ApplicationUser", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("Weterynarz.Domain.EntitiesDb.MedicalExaminationType", "MedicalExaminationType")
                        .WithMany()
                        .HasForeignKey("MedicalExaminationTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Weterynarz.Domain.EntitiesDb.Visit", b =>
                {
                    b.HasOne("Weterynarz.Domain.EntitiesDb.Animal", "Animal")
                        .WithMany()
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Weterynarz.Domain.EntitiesDb.ApplicationUser", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
