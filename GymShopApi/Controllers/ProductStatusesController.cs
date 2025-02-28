using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductStatusesController(IGenericService<ProductStatus> productStatusService) : ControllerBase
{
    private readonly IGenericService<ProductStatus> _productStatusService = productStatusService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productStatuses = await _productStatusService.GetAllAsync();

        if (!productStatuses.Any())
        {
            return NotFound("No product statuses found");
        }

        return Ok(productStatuses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var productStatus = await _productStatusService.GetByIdAsync(id);

        if (productStatus == null)
        {
            return NotFound($"No product status found with ID: {id}");
        }

        return Ok(productStatus);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductStatus productStatus)
    {
        try
        {
            var newProductStatus = await _productStatusService.AddAsync(productStatus);
            return CreatedAtAction(nameof(Get), new { id = newProductStatus.Id }, newProductStatus);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductStatus updatedProductStatus)
    {
        try
        {
            var productStatus = await _productStatusService.Update(id, updatedProductStatus);
            return Ok(productStatus);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Product status not found");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productStatusService.Delete(id);
            return Ok("Product status deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Product status not found");
        }
    }
}
