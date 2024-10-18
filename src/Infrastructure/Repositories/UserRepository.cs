using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces;
using EmploymentSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories
{
  public class UserRepository : IRepository<User>
  {
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<User> GetByIdAsync(int id)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
      if (user == null)
      {
        throw new KeyNotFoundException($"User with ID {id} not found.");
      }
      return user;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
      if (user == null)
      {
        throw new KeyNotFoundException($"User with Email {email} not found.");
      }
      return user;
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
      var user = await _context.Users.FindAsync(username);
      if (user == null)
      {
        throw new KeyNotFoundException($"User with Username {username} not found.");
      }
      return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User entity)
    {
      await _context.Users.AddAsync(entity);
    }

    public void Update(User entity)
    {
      _context.Users.Update(entity);
    }

    public void Delete(User entity)
    {
      _context.Users.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}