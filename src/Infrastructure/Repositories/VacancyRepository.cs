using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces;
using EmploymentSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories
{
  public class VacancyRepository : IRepository<Vacancy>
  {
    private readonly ApplicationDbContext _context;

    public VacancyRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Vacancy> GetByIdAsync(int id)
    {
      var vacancy = await _context.Vacancies.Include(v => v.Employer)
                                     .Include(v => v.Applications)
                                     .FirstOrDefaultAsync(v => v.Id == id);
      if (vacancy == null)
      {
        throw new KeyNotFoundException($"Vacancy with ID ${id} not found.");
      }
      return vacancy;
    }

    public async Task<IEnumerable<Vacancy>> GetAllAsync()
    {
      return await _context.Vacancies.Include(v => v.Employer).ToListAsync();
    }

    public async Task AddAsync(Vacancy entity)
    {
      await _context.Vacancies.AddAsync(entity);
    }

    public void Update(Vacancy entity)
    {
      _context.Vacancies.Update(entity);
    }

    public void Delete(Vacancy entity)
    {
      _context.Vacancies.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}