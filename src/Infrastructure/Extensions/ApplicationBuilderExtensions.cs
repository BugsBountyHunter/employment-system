using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EmploymentSystem.Infrastructure.Extensions
{
  public static class ApplicationBuilderExtensions
  {
    public static void UseCustomMiddlewares(this IApplicationBuilder app)
    {
      app.UseAuthentication();
      app.UseAuthorization();
    }
  }
}
