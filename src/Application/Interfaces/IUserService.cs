using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Application.Interfaces
{
  public interface IUserService
  {
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByUsernameAsync(string username);
  }
}
