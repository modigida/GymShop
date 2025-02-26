using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductStatusesController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productStatuses = await _unitOfWork.ProductStatuses.GetAllAsync();

        if (!productStatuses.Any())
        {
            return NotFound("No product statuses found");
        }

        await _unitOfWork.CompleteAsync();
        return Ok(productStatuses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var productStatus = await _unitOfWork.ProductStatuses.GetByIdAsync(id);

        if (productStatus == null)
        {
            return NotFound($"No product status found with ID: {id}");
        }

        await _unitOfWork.CompleteAsync();
        return Ok(productStatus);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductStatus productStatus)
    {
        if (productStatus == null || string.IsNullOrEmpty(productStatus.Name))
        {
            return BadRequest("Invalid input");
        }

        await _unitOfWork.ProductStatuses.AddAsync(productStatus);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(Get), new { id = productStatus.Id }, productStatus);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductStatus updatedProductStatus)
    {
        var productStatus = await _unitOfWork.ProductStatuses.GetByIdAsync(id);
        if (productStatus == null)
        {
            return NotFound("Product status not found");
        }

        if (updatedProductStatus == null || string.IsNullOrEmpty(updatedProductStatus.Name))
        {
            return BadRequest("Invalid input");
        }

        productStatus.Name = updatedProductStatus.Name;

        _unitOfWork.ProductStatuses.Update(productStatus);
        await _unitOfWork.CompleteAsync();
        return Ok(productStatus);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var productStatus = await _unitOfWork.ProductStatuses.GetByIdAsync(id);
        if (productStatus == null)
        {
            return NotFound("Product status not found");
        }
        _unitOfWork.ProductStatuses.Delete(productStatus);
        await _unitOfWork.CompleteAsync();
        return Ok("Product status deleted");
    }
}
