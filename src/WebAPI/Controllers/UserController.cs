using EmploymentSystem.Application.Services;
using EmploymentSystem.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
      _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto userDto)
    {
      var user = await _userService.RegisterAsync(userDto.Username, userDto.Email, userDto.Password, userDto.Role);
      return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
      var token = await _userService.LoginAsync(loginDto.Email, loginDto.Password);
      return Ok(new { Token = token });
    }
  }
}