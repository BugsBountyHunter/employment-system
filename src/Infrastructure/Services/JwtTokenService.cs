using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmploymentSystem.Infrastructure.Services
{
  public interface IJwtTokenService
  {
    string GenerateJwtToken(string userId, string role);
  }

  public class JwtTokenService : IJwtTokenService
  {
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string GenerateJwtToken(string userId, string role)
    {
      var jwtSettings = _configuration.GetSection("JwtSettings");
      var secretKey = jwtSettings["SecretKey"];
      if (secretKey == null)
      {
        throw new InvalidOperationException("JWT secret not found in configuration.");
      }
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique ID for the token
            };

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
        SigningCredentials = creds,
        Issuer = jwtSettings["Issuer"],
        Audience = jwtSettings["Audience"]
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      try
      {
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw new UnauthorizedAccessException("Failed to generate token");
      }

    }
  }
}