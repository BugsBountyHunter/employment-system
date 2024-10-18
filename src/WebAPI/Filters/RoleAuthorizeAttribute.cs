using EmploymentSystem.Application.Interfaces;
using EmploymentSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
{
  private readonly UserRole _role;

  public RoleAuthorizeAttribute(UserRole role)
  {
    _role = role;
  }

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    var userService = context.HttpContext.RequestServices.GetService<IUserService>();

    // Get the user ID from the JWT token
    var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
    var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

    if (userIdClaim == null)
    {
      context.Result = new UnauthorizedResult();
      return;
    }


    if (userService == null)
    {
      context.Result = new UnauthorizedResult();
      return;
    }

    // Fetch the user from the database
    var user = userService.GetUserByIdAsync(int.Parse(userIdClaim.Value)).Result;

    // Check if the user has the correct role
    if (user == null || user.Role != _role)
    {
      context.Result = new ForbidResult();
    }
  }
}
