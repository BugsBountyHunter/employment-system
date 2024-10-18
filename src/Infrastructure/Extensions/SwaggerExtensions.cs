using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace EmploymentSystem.Infrastructure.Extensions
{
  public static class SwaggerExtensions
  {
    // Swagger configuration
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen(); // Optionally
      return services;
    }

    // Middleware for using Swagger
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employment API V1");
      });
      return app;
    }
  }
}
