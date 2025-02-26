using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _unitOfWork.Users.GetAllAsync();

        if (!users.Any())
        {
            return NotFound("No users found");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound($"No user found with ID: {id}");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        if (user == null || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || user.RoleId == 0 ||
            string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.PasswordSalt) || string.IsNullOrEmpty(user.Email) ||
            string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Address))
        {
            return BadRequest("Invalid input");
        }

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] User updatedUser)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        if (updatedUser == null)
        {
            return BadRequest("Invalid input");
        }

        if (!string.IsNullOrEmpty(updatedUser.FirstName)) { user.FirstName = updatedUser.FirstName; }
        if (!string.IsNullOrEmpty(updatedUser.LastName)) { user.LastName = updatedUser.LastName; }
        if (updatedUser.RoleId != 0) { user.RoleId = updatedUser.RoleId; }
        if (!string.IsNullOrEmpty(updatedUser.PasswordHash)) { user.PasswordHash = updatedUser.PasswordHash; }
        if (!string.IsNullOrEmpty(updatedUser.PasswordSalt)) { user.PasswordSalt = updatedUser.PasswordSalt; }
        if (!string.IsNullOrEmpty(updatedUser.Email)) { user.Email = updatedUser.Email; }
        if (!string.IsNullOrEmpty(updatedUser.Phone)) { user.Phone = updatedUser.Phone; }
        if (!string.IsNullOrEmpty(updatedUser.Address)) { user.Address = updatedUser.Address; }

        _unitOfWork.Users.Update(user);
        await _unitOfWork.CompleteAsync();
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        _unitOfWork.Users.Delete(user);
        await _unitOfWork.CompleteAsync();
        return Ok("User deleted");
    }

}
