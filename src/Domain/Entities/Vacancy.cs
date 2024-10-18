namespace EmploymentSystem.Domain.Entities
{
  public class Vacancy
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int MaxApplications { get; set; }
    public VacancyStatus Status { get; set; } = VacancyStatus.Active;

    //Foreign key
    public int EmployerId { get; set; }

    // Navigation Properties
    public required User Employer { get; set; }
    public ICollection<JobApplication>? Applications { get; set; }
  }

  public enum VacancyStatus
  {
    Active,
    Inactive,
    Archived
  }
}