﻿// <auto-generated />
using System;
using EmploymentSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmploymentSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmploymentSystem.Domain.Entities.JobApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VacancyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("VacancyId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("EmploymentSystem.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EmploymentSystem.Domain.Entities.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxApplications")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("EmploymentSystem.Domain.Entities.JobApplication", b =>
                {
                    b.HasOne("EmploymentSystem.Domain.Entities.User", "Applicant")
                        .WithMany("Applications")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EmploymentSystem.Domain.Entities.Vacancy", "Vacancy")
                        .WithMany("Applications")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("EmploymentSystem.Domain.Entities.Vacancy", b =>
                {
                    b.HasOne("EmploymentSystem.Domain.Entities.User", "Employer")
                        .WithMany("Vacancies")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("EmploymentSystem.Domain.Entities.User", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("EmploymentSystem.Domain.Entities.Vacancy", b =>
                {
                    b.Navigation("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}
