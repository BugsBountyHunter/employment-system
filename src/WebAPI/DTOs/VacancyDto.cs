using EmploymentSystem.Domain.Entities;
using EmploymentSystem.WebApi.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.WebApi.Dtos
{
  public class VacancyDto
  {
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Expiry date is required")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    [FutureDate(ErrorMessage = "Expiry date must be a future date")]
    public DateTime ExpiryDate { get; set; }

    [Required(ErrorMessage = "Max applications is required")]
    [Range(1, 1000, ErrorMessage = "Max applications must be between 1 and 1000")]
    public int MaxApplications { get; set; }

    public VacancyStatus? Status { get; set; } = VacancyStatus.Active;
  }
}
