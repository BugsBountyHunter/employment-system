namespace EmploymentSystem.Domain.Entities
{

  public class User
  {
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; }

    // Navigation Properties
    public ICollection<Vacancy>? Vacancies { get; set; }  // in case employer
    public ICollection<JobApplication>? Applications { get; set; } // in case applicant
  }
  public enum UserRole
  {
    Employer,
    Applicant
  }

}