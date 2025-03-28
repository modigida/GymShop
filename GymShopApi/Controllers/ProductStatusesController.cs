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
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productStatuses = await productStatusService.GetAllAsync();

        if (!productStatuses.Any())
        {
            return NotFound("No product statuses found");
        }

        return Ok(productStatuses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var productStatus = await productStatusService.GetByIdAsync(id);

        if (productStatus == null)
        {
            return NotFound($"No product status found with ID: {id}");
        }

        return Ok(productStatus);
    }

    //[HttpPost]
    //public async Task<IActionResult> Post([FromBody] ProductStatus productStatus)
    //{
    //    try
    //    {
    //        var newProductStatus = await productStatusService.AddAsync(productStatus);
    //        return CreatedAtAction(nameof(Get), new { id = newProductStatus.Id }, newProductStatus);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> Put(int id, [FromBody] ProductStatus updatedProductStatus)
    //{
    //    try
    //    {
    //        var productStatus = await productStatusService.Update(updatedProductStatus, id);
    //        return Ok(productStatus);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound("Product status not found");
    //    }
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    try
    //    {
    //        await productStatusService.Delete(id);
    //        return Ok("Product status deleted");
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound("Product status not found");
    //    }
    //}
}
