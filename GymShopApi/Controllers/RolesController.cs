using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IUnitOfWork unitOfWork, IGenericRepository<Role> roleRepository) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<Role> _roleRepository = roleRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _roleRepository.GetAllAsync();

        if (!roles.Any())
        {
            return NotFound("No roles found");
        }

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);

        if (role == null)
        {
            return NotFound($"No role found with ID: {id}");
        }

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Role role)
    {
        if (role == null || string.IsNullOrEmpty(role.Name))
        {
            return BadRequest("Invalid input");
        }

        await _roleRepository.AddAsync(role);

        return CreatedAtAction(nameof(GetAll), new { id = role.Id }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Role updatedRole)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
        {
            return NotFound("Role not found");
        }

        if (updatedRole == null || string.IsNullOrEmpty(updatedRole.Name))
        {
            return BadRequest("Invalid input");
        }

        role.Name = updatedRole.Name;

        _roleRepository.Update(role);

        return Ok(role);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
        {
            return NotFound("Role not found");
        }
        _roleRepository.Delete(role);

        return Ok("Role deleted");
    }

}
