using System;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.Application.DTOs
{
  public class JobApplicationDto
  {
    [Required(ErrorMessage = "VacancyId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "VacancyId must be a positive number.")]
    public int VacancyId { get; set; }

    [Required(ErrorMessage = "ApplicantId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "ApplicantId must be a positive number.")]
    public int ApplicantId { get; set; }

  }
}
