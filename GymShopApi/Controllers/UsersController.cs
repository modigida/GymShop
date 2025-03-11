using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(UserCreateDto dto)
    {
        try
        {
            var user = await _userService.RegisterUserAsync(dto);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<bool>> Login(UserLoginDto dto)
    {
        var isValid = await _userService.LoginUserAsync(dto);
        if (!isValid)
            return Unauthorized(new { message = "Invalid email or password" });

        return Ok(new { message = "Login successful" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();

        if (!users.Any())
        {
            return NotFound("No users found");
        }

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound($"No user found with ID: {id}");
        }

        return Ok(user);
    }

    //[HttpPost]
    //public async Task<IActionResult> Post([FromBody] User user)
    //{
    //    try
    //    {
    //        var newUser = await _userService.AddAsync(user);
    //        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UserCreateDto updatedUser)
    {
        try
        {
            var user = await _userService.Update(updatedUser, id);
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
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _userService.Delete(id);
            return Ok("User deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User not found");
        }
    }

}
