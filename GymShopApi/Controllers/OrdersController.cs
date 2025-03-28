using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await orderService.GetAllAsync();

        if (!orders.Any())
        {
            return NotFound("No orders found");
        }

        return Ok(orders);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var order = await orderService.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound($"No order found with ID: {id}");
        }

        return Ok(order);
    }

    //[HttpGet("email/{email}")]
    //public async Task<IActionResult> GetByEmail(string email)
    //{
    //    var orders = await orderService.GetByEmailAsync(email);

    //    if (!orders.Any())
    //    {
    //        return NotFound($"No orders found for email: {email}");
    //    }

    //    return Ok(orders);
    //}

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderCreateDto orderResponse)
    {
        try
        {
            var newOrder = await orderService.AddAsync(orderResponse);
            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrderCreateDto updatedOrderResponse)
    {
        try
        {
            var order = await orderService.Update(updatedOrderResponse, id);
            return Ok(order);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Order not found");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await orderService.Delete(id);
            return Ok("Order deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Order not found");
        }
        catch (ArgumentException)
        {
            return NotFound("Order not found");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
