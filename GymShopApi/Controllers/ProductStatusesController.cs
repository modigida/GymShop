using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductStatusesController(IUnitOfWork unitOfWork, IGenericRepository<ProductStatus> productStatusRepository) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<ProductStatus> _productStatusRepository = productStatusRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productStatuses = await _productStatusRepository.GetAllAsync();

        if (!productStatuses.Any())
        {
            return NotFound("No product statuses found");
        }

        return Ok(productStatuses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var productStatus = await _productStatusRepository.GetByIdAsync(id);

        if (productStatus == null)
        {
            return NotFound($"No product status found with ID: {id}");
        }

        return Ok(productStatus);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductStatus productStatus)
    {
        if (productStatus == null || string.IsNullOrEmpty(productStatus.Name))
        {
            return BadRequest("Invalid input");
        }

        await _productStatusRepository.AddAsync(productStatus);

        return CreatedAtAction(nameof(GetAll), new { id = productStatus.Id }, productStatus);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Role updatedProductStatus)
    {
        var productStatus = await _productStatusRepository.GetByIdAsync(id);
        if (productStatus == null)
        {
            return NotFound("Product status not found");
        }

        if (updatedProductStatus == null || string.IsNullOrEmpty(updatedProductStatus.Name))
        {
            return BadRequest("Invalid input");
        }

        productStatus.Name = updatedProductStatus.Name;

        _productStatusRepository.Update(productStatus);

        return Ok(productStatus);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var productStatus = await _productStatusRepository.GetByIdAsync(id);
        if (productStatus == null)
        {
            return NotFound("Product status not found");
        }
        _productStatusRepository.Delete(productStatus);

        return Ok("Product status deleted");
    }
}
