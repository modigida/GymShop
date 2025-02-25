using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();

        if (!categories.Any())
        {
            return NotFound("No categories found");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound($"No category found with ID: {id}");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Category category)
    {
        if (category == null || string.IsNullOrEmpty(category.Name))
        {
            return BadRequest("Invalid input");
        }

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Category updatedCategory)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        if (updatedCategory == null || string.IsNullOrEmpty(updatedCategory.Name))
        {
            return BadRequest("Invalid input");
        }

        category.Name = updatedCategory.Name;

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.CompleteAsync();
        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }
        _unitOfWork.Categories.Delete(category);
        await _unitOfWork.CompleteAsync();
        return Ok("Category deleted");
    }
}
