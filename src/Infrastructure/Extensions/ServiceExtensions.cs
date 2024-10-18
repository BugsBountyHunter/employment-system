using EmploymentSystem.Application.Interfaces;
using EmploymentSystem.Application.Services;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces;
using EmploymentSystem.Infrastructure.Repositories;
using EmploymentSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmploymentSystem.Infrastructure.Extensions
{
  public static class ServiceExtensions
  {
    // Register JWT Token Service
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
      var jwtSecret = configuration["JwtSettings:SecretKey"];
      if (jwtSecret == null)
      {
        throw new InvalidOperationException("JWT secret not found in configuration.");
      }
      var key = Encoding.ASCII.GetBytes(jwtSecret);

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidIssuer = configuration["JwtSettings:Issuer"],
          ValidAudience = configuration["JwtSettings:Audience"],
          ClockSkew = TimeSpan.Zero
        };
      });

      return services;
    }

    // Register all other services like JwtTokenService
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddScoped<IJwtTokenService, JwtTokenService>();
      services.AddScoped<UserService>();
      services.AddScoped<VacancyService>();
      services.AddScoped<UserRepository, UserRepository>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<VacancyRepository, VacancyRepository>();
      services.AddScoped<IRepository<JobApplication>, JobApplicationRepository>();
      services.AddControllers()
          .AddJsonOptions(options =>
          {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            options.JsonSerializerOptions.WriteIndented = true;
          });

      return services;
    }
  }
}
