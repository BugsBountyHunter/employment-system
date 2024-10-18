using EmploymentSystem.Application.Services;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmploymentSystem.WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class VacancyController : ControllerBase
  {
    private readonly VacancyService _vacancyService;
    private readonly UserService _UserService;

    public VacancyController(VacancyService vacancyService, UserService userService)
    {
      _vacancyService = vacancyService;
      _UserService = userService;
    }


    [HttpPost]
    [RoleAuthorize(UserRole.Employer)]
    public async Task<IActionResult> CreateVacancy(VacancyDto CreateVacancyDto)
    {
      var employerId = GetEmployerId();
      var employer = await _UserService.GetUserByIdAsync(employerId);

      if (employer == null || employer.Role != UserRole.Employer)
        return Forbid();

      var vacancy = new Vacancy
      {
        Title = CreateVacancyDto.Title,
        Description = CreateVacancyDto.Description,
        ExpiryDate = CreateVacancyDto.ExpiryDate,
        MaxApplications = CreateVacancyDto.MaxApplications,
        Status = CreateVacancyDto.Status ?? VacancyStatus.Active, // Set default status
        EmployerId = employerId, // Use the employer ID from the token
        Employer = employer // Set the Employer property
      };

      await _vacancyService.CreateVacancyAsync(vacancy);
      return CreatedAtAction(nameof(GetVacancyById), new { id = vacancy.Id }, vacancy);
    }

    [HttpGet("{id}")]
    [RoleAuthorize(UserRole.Employer)]
    public async Task<IActionResult> GetVacancyById(int id)
    {
      var vacancy = await _vacancyService.GetVacancyByIdAsync(id);
      return Ok(vacancy);
    }

    [HttpPut("{id}")]
    [RoleAuthorize(UserRole.Employer)]
    public async Task<IActionResult> UpdateVacancy(int id, VacancyDto vacancyDto)
    {
      var employerId = GetEmployerId();
      var vacancy = await _vacancyService.GetVacancyByIdAsync(id);
      if (vacancy == null)
        return NotFound();

      // Check if the current user is the employer of the vacancy
      if (vacancy.EmployerId != employerId)
        return Forbid();

      // Update the vacancy
      vacancy.Title = vacancyDto.Title;
      vacancy.Description = vacancyDto.Description;
      vacancy.ExpiryDate = vacancyDto.ExpiryDate;
      vacancy.MaxApplications = vacancyDto.MaxApplications;

      await _vacancyService.UpdateVacancyAsync(vacancy);
      return NoContent();
    }

    [HttpDelete("{id}")]
    [RoleAuthorize(UserRole.Employer)]
    public async Task<IActionResult> DeleteVacancy(int id)
    {
      var employerId = GetEmployerId();
      var vacancy = await _vacancyService.GetVacancyByIdAsync(id);
      if (vacancy == null)
        return NotFound();

      // Check if the current user is the employer of the vacancy
      if (vacancy.EmployerId != employerId)
        return Forbid();

      await _vacancyService.DeleteVacancyAsync(vacancy);
      return NoContent();
    }

    // Helper method to get the employer's ID from the token
    private int GetEmployerId()
    {
      var claimsIdentity = User.Identity as ClaimsIdentity;
      var employerIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier); // Assuming NameIdentifier is used for user ID

      if (employerIdClaim == null)
      {
        throw new UnauthorizedAccessException("User ID not found in claims.");
      }

      return int.Parse(employerIdClaim.Value);
    }
  }
}
