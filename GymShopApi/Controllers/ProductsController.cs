using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _unitOfWork.Products.GetAllAsync();

        if (!products.Any())
        {
            return NotFound("No products found");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound($"No product found with ID: {id}");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        if (product == null || string.IsNullOrEmpty(product.Name) || product.Balance == 0 || product.Price == 0)
        {
            return BadRequest("Invalid input");
        }

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product updatedProduct)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }

        if (updatedProduct == null)
        {
            return BadRequest("Invalid input");
        }

        if (!string.IsNullOrEmpty(updatedProduct.Name)) { product.Name = updatedProduct.Name; }
        if (updatedProduct.CategoryId != 0) { product.CategoryId = updatedProduct.CategoryId; }
        if (updatedProduct.ProductStatusId != 0) { product.ProductStatusId = updatedProduct.ProductStatusId; }
        if (updatedProduct.Balance != 0) { product.Balance = updatedProduct.Balance;}
        if (updatedProduct.Price != 0) { product.Price = updatedProduct.Price; }
        if (!string.IsNullOrEmpty(updatedProduct.Description)) { product.Description = updatedProduct.Description; }

        _unitOfWork.Products.Update(product);
        await _unitOfWork.CompleteAsync();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }
        _unitOfWork.Products.Delete(product);
        await _unitOfWork.CompleteAsync();
        return Ok("Product deleted");
    }

}
