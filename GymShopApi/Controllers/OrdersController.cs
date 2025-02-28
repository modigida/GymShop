using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IGenericService<Order> orderService) : ControllerBase
{
    private readonly IGenericService<Order> _orderService = orderService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllAsync();

        if (!orders.Any())
        {
            return NotFound("No orders found");
        }

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var order = await _orderService.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound($"No order found with ID: {id}");
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Order order)
    {
        try
        {
            var newOrder = await _orderService.AddAsync(order);
            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Order updatedOrder)
    {
        try
        {
            var category = await _orderService.Update(id, updatedOrder);
            return Ok(category);
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
            await _orderService.Delete(id);
            return Ok("Order deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Order not found");
        }
    }
}
