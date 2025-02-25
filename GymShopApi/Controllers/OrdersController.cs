using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IUnitOfWork unitOfWork, IGenericRepository<Order> orderRepository) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<Order> _orderRepository = orderRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderRepository.GetAllAsync();

        if (!orders.Any())
        {
            return NotFound("No orders found");
        }

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound($"No order found with ID: {id}");
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Order order)
    {
        if (order == null || order.UserId == null || order.OrderStatusId == 0 || order.PurchaseDate == null)
        {
            return BadRequest("Invalid input");
        }

        await _orderRepository.AddAsync(order);

        return CreatedAtAction(nameof(GetAll), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Order updatedOrder)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound("Order not found");
        }

        if (updatedOrder == null || updatedOrder.OrderStatusId == null)
        {
            return BadRequest("Invalid input");
        }

        order.OrderStatusId = updatedOrder.OrderStatusId;

        _orderRepository.Update(order);

        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound("Order not found");
        }
        _orderRepository.Delete(order);

        return Ok("Order deleted");
    }
}
