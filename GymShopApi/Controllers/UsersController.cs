using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(UserCreateDto dto)
    {
        try
        {
            var user = await userService.RegisterUserAsync(dto);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidatePassword([FromBody] UserLoginDto dto)
    {
        var isValid = await userService.ValidatePasswordAsync(dto.Email, dto.Password);
        return Ok(isValid);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UserLoginDto dto)
    {
        var token = await userService.LoginUserAsync(dto);

        if (token == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        return Ok(new { token });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await userService.GetAllAsync();

        if (!users.Any())
        {
            return NotFound("No users found");
        }

        return Ok(users);
    }    
    
    [HttpGet("customers")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var users = await userService.GetAllCustomersAsync();

        if (!users.Any())
        {
            return NotFound("No users found");
        }

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await userService.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound($"No user found with ID: {id}");
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(Guid id, [FromBody] UserCreateDto updatedUser)
    {
        try
        {
            var user = await userService.Update(updatedUser, id);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User not found");
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await userService.Delete(id);
            return Ok("User deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User not found");
        }
    }

}
