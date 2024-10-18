using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces;
using EmploymentSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories
{
  public class JobApplicationRepository : IRepository<JobApplication>
  {
    private readonly ApplicationDbContext _context;

    public JobApplicationRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<JobApplication> GetByIdAsync(int id)
    {
      var application = await _context.Applications.Include(a => a.Vacancy)
                                        .Include(a => a.Applicant)
                                        .FirstOrDefaultAsync(a => a.Id == id);

      if (application == null)
      {
        throw new KeyNotFoundException($"Application with ID ${id} not found.");
      }
      return application;
    }

    public async Task<IEnumerable<JobApplication>> GetAllAsync()
    {
      return await _context.Applications.Include(a => a.Vacancy).ToListAsync();
    }

    public async Task AddAsync(JobApplication entity)
    {
      await _context.Applications.AddAsync(entity);
    }

    public void Update(JobApplication entity)
    {
      _context.Applications.Update(entity);
    }

    public void Delete(JobApplication entity)
    {
      _context.Applications.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}