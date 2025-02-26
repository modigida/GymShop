using GymShopApi.Entities;
using Microsoft.AspNetCore.Mvc;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IGenericService<Category> categoryService) : ControllerBase
{
    private readonly IGenericService<Category> _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        if (!categories.Any())
        {
            return NotFound("No categories found");
        }
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound($"No category found with ID: {id}");
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Category category)
    {
        try
        {
            var newCategory = await _categoryService.AddAsync(category);
            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Category updatedCategory)
    {
        try
        {
            var category = await _categoryService.Update(id, updatedCategory);
            return Ok(category);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Category not found");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Category category)
    {
        try
        {
            await _categoryService.Delete(category);
            return Ok("Category deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Category not found");
        }
    }
}
