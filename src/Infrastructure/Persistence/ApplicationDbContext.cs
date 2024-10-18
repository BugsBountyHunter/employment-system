using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Persistence
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<JobApplication> Applications { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      // Configuring entity relationships and constraints
      builder.Entity<User>()
                     .HasIndex(u => u.Email)
                     .IsUnique();

      builder.Entity<User>()
                     .HasIndex(u => u.Username)
                     .IsUnique();

      builder.Entity<Vacancy>()
                     .HasOne(v => v.Employer)
                     .WithMany(u => u.Vacancies)
                     .HasForeignKey(v => v.EmployerId);

      builder.Entity<JobApplication>()
                      .HasOne(a => a.Vacancy)
                      .WithMany(v => v.Applications)
                      .HasForeignKey(a => a.VacancyId)
                      .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<JobApplication>()
                      .HasOne(a => a.Applicant)
                      .WithMany(u => u.Applications)
                      .HasForeignKey(a => a.ApplicantId)
                      .OnDelete(DeleteBehavior.Restrict);

    }
  }


}