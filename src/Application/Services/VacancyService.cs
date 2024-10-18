using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces;
using EmploymentSystem.Infrastructure.Repositories;

namespace EmploymentSystem.Application.Services
{
  public class VacancyService
  {
    private readonly VacancyRepository _vacancyRepository;

    public VacancyService(VacancyRepository vacancyRepository)
    {
      _vacancyRepository = vacancyRepository;
    }

    public async Task<IEnumerable<Vacancy>> GetAllVacanciesAsync()
    {
      return await _vacancyRepository.GetAllAsync();
    }

    public async Task<Vacancy> GetVacancyByIdAsync(int id)
    {
      return await _vacancyRepository.GetByIdAsync(id);
    }

    public async Task CreateVacancyAsync(Vacancy vacancy)
    {
      await _vacancyRepository.AddAsync(vacancy);
      await _vacancyRepository.SaveChangesAsync();
    }

    public async Task UpdateVacancyAsync(Vacancy vacancy)
    {
      _vacancyRepository.Update(vacancy);
      await _vacancyRepository.SaveChangesAsync();
    }

    public async Task DeleteVacancyAsync(Vacancy vacancy)
    {
      _vacancyRepository.Delete(vacancy);
      await _vacancyRepository.SaveChangesAsync();
    }
  }
}
