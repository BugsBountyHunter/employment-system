using EmploymentSystem.Infrastructure.Extensions;
using EmploymentSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Use the extension methods for registering services
// Use extension methods for better separation of concerns
builder.Services.AddApplicationServices(); // JwtTokenService, repositories, etc.
builder.Services.AddJwtAuthentication(builder.Configuration); // JWT authentication
builder.Services.AddDatabase(builder.Configuration); // DbContext configuration
builder.Services.AddSwaggerDocumentation(); // Swagger configuration


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Global error handling for production
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
