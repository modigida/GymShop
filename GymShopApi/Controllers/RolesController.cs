using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;
using GymShopApi.Services.Interfaces;
using GymShopApi.Services;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IGenericService<Role> roleService) : ControllerBase
{
    private readonly IGenericService<Role> _roleService = roleService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _roleService.GetAllAsync();

        if (!roles.Any())
        {
            return NotFound("No roles found");
        }

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _roleService.GetByIdAsync(id);

        if (role == null)
        {
            return NotFound($"No role found with ID: {id}");
        }

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Role role)
    {
        try
        {
            var newRole = await _roleService.AddAsync(role);
            return CreatedAtAction(nameof(Get), new { id = newRole.Id }, newRole);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Role updatedRole)
    {
        try
        {
            var role = await _roleService.Update(id, updatedRole);
            return Ok(role);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Role not found");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _roleService.Delete(id);
            return Ok("Role deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Role not found");
        }
    }

}
