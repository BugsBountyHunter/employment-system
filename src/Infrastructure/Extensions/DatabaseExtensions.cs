using EmploymentSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmploymentSystem.Infrastructure.Extensions
{
  public static class DatabaseExtensions
  {
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

      return services;
    }
  }
}
