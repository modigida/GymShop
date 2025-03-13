using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllAsync();

        if (!products.Any())
        {
            return NotFound("No products found");
        }

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound($"No product found with ID: {id}");
        }

        return Ok(product);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var products = await productService.GetByCategoryAsync(categoryId);
        if (!products.Any())
        {
            return NotFound($"No products found for category with ID: {categoryId}");
        }
        return Ok(products);
    }

    [HttpGet("status/{statusId}")]
    public async Task<IActionResult> GetByStatus(int statusId)
    {
        var products = await productService.GetByStatusAsync(statusId);
        if (!products.Any())
        {
            return NotFound($"No products found for status with ID: {statusId}");
        }
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDto product)
    {
        try
        {
            var newProduct = await productService.AddAsync(product);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductDto updatedProduct)
    {
        try
        {
            var product = await productService.Update( updatedProduct, id);
            return Ok(product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Product not found");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await productService.Delete(id);
            return Ok("Product deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Product not found");
        }
    }

}
