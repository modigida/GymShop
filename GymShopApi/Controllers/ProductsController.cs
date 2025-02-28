using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericService<Product> productService) : ControllerBase
{
    private readonly IGenericService<Product> _productService = productService;
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        if (!products.Any())
        {
            return NotFound("No products found");
        }

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound($"No product found with ID: {id}");
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        try
        {
            var newProduct = await _productService.AddAsync(product);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product updatedProduct)
    {
        try
        {
            var product = await _productService.Update(id, updatedProduct);
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
            await _productService.Delete(id);
            return Ok("Product deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Product not found");
        }
    }

}
