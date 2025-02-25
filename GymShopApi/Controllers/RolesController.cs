using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _unitOfWork.Roles.GetAllAsync();

        if (!roles.Any())
        {
            return NotFound("No roles found");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(id);

        if (role == null)
        {
            return NotFound($"No role found with ID: {id}");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Role role)
    {
        if (role == null || string.IsNullOrEmpty(role.Name))
        {
            return BadRequest("Invalid input");
        }

        await _unitOfWork.Roles.AddAsync(role);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetAll), new { id = role.Id }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Role updatedRole)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(id);
        if (role == null)
        {
            return NotFound("Role not found");
        }

        if (updatedRole == null || string.IsNullOrEmpty(updatedRole.Name))
        {
            return BadRequest("Invalid input");
        }

        role.Name = updatedRole.Name;

        _unitOfWork.Roles.Update(role);
        await _unitOfWork.CompleteAsync();
        return Ok(role);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(id);
        if (role == null)
        {
            return NotFound("Role not found");
        }
        _unitOfWork.Roles.Delete(role);
        await _unitOfWork.CompleteAsync();
        return Ok("Role deleted");
    }

}
