using System.Security.Cryptography;
using System.Text;
using EmploymentSystem.Application.Interfaces;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces;
using EmploymentSystem.Infrastructure.Repositories;
using EmploymentSystem.Infrastructure.Services;

namespace EmploymentSystem.Application.Services
{
  public class UserService : IUserService
  {
    private readonly UserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public UserService(UserRepository userRepository, IJwtTokenService jwtTokenService)
    {
      _userRepository = userRepository;
      _jwtTokenService = jwtTokenService;
    }

    public async Task<User> RegisterAsync(string username, string email, string password, UserRole role)
    {
      var user = new User
      {
        Username = username,
        Email = email,
        PasswordHash = HashPassword(password),
        Role = role
      };

      await _userRepository.AddAsync(user);
      await _userRepository.SaveChangesAsync();

      return user;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
      var user = await _userRepository.GetByEmailAsync(email);

      if (user == null || !VerifyPassword(password, user.PasswordHash)) // VerifyPassword would be implemented
        throw new UnauthorizedAccessException("Invalid credentials");

      // Generate JWT token
      return _jwtTokenService.GenerateJwtToken(user.Id.ToString(), user.Role.ToString());
    }

    private string HashPassword(string password)
    {
      using (var sha256 = SHA256.Create())
      {
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
      }
    }

    private bool VerifyPassword(string password, string storedHash)
    {
      var hash = HashPassword(password);
      return hash == storedHash;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
      return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
      return await _userRepository.GetByUsernameAsync(username);
    }
  }

}