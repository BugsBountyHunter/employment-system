namespace EmploymentSystem.Domain.Entities
{
  public class JobApplication
  {
    public int Id { get; set; }

    // foreign key
    public int VacancyId { get; set; }
    public int ApplicantId { get; set; }

    // Navigation Properties
    public required Vacancy Vacancy { get; set; }
    public required User Applicant { get; set; }


    public DateTime ApplicationDate { get; set; }
  }
}