using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderStatusesController(IGenericService<OrderStatus> orderStatusService) : ControllerBase
{
    private readonly IGenericService<OrderStatus> _orderStatusService = orderStatusService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orderStatuses = await _orderStatusService.GetAllAsync();

        if (!orderStatuses.Any())
        {
            return NotFound("No order statuses found");
        }

        return Ok(orderStatuses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var orderStatus = await _orderStatusService.GetByIdAsync(id);

        if (orderStatus == null)
        {
            return NotFound($"No order status found with ID: {id}");
        }

        return Ok(orderStatus);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderStatus orderStatus)
    {
        try
        {
            var newOrderStatus = await _orderStatusService.AddAsync(orderStatus);
            return CreatedAtAction(nameof(Get), new { id = newOrderStatus.Id }, newOrderStatus);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrderStatus updatedOrderStatus)
    {
        try
        {
            var orderStatus = await _orderStatusService.Update(id, updatedOrderStatus);
            return Ok(orderStatus);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Order status not found");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(OrderStatus orderStatus)
    {
        try
        {
            await _orderStatusService.Delete(orderStatus);
            return Ok("Order status deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Order status not found");
        }
    }
}
