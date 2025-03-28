using GymShopApi.Entities;
using Microsoft.AspNetCore.Mvc;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IGenericService<Category> categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await categoryService.GetAllAsync();

        if (!categories.Any())
        {
            return NotFound("No categories found");
        }
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var category = await categoryService.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound($"No category found with ID: {id}");
        }

        return Ok(category);
    }

    //[HttpPost]
    //public async Task<IActionResult> Post([FromBody] Category category)
    //{
    //    try
    //    {
    //        var newCategory = await categoryService.AddAsync(category);
    //        return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> Put(int id, [FromBody] Category updatedCategory)
    //{
    //    try
    //    {
    //        var category = await categoryService.Update(updatedCategory, id);
    //        return Ok(category);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound("Category not found");
    //    }
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    try
    //    {
    //        await categoryService.Delete(id);
    //        return Ok("Category deleted");
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound("Category not found");
    //    }
    //}
}
