using GymShopApi.DTOs;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymShopApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var token = await userService.LoginUserAsync(dto);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }
        return Ok(new { Token = token });
    }
}
