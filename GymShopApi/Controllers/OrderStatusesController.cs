using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderStatusesController(IUnitOfWork unitOfWork, IGenericRepository<OrderStatus> orderStatusRepository) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<OrderStatus> _orderStatusRepository = orderStatusRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orderStatuses = await _orderStatusRepository.GetAllAsync();

        if (!orderStatuses.Any())
        {
            return NotFound("No order statuses found");
        }

        return Ok(orderStatuses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var orderStatus = await _orderStatusRepository.GetByIdAsync(id);

        if (orderStatus == null)
        {
            return NotFound($"No order status found with ID: {id}");
        }

        return Ok(orderStatus);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderStatus orderStatus)
    {
        if (orderStatus == null || string.IsNullOrEmpty(orderStatus.Name))
        {
            return BadRequest("Invalid input");
        }

        await _orderStatusRepository.AddAsync(orderStatus);

        return CreatedAtAction(nameof(GetAll), new { id = orderStatus.Id }, orderStatus);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrderStatus updatedOrderStatus)
    {
        var orderStatus = await _orderStatusRepository.GetByIdAsync(id);
        if (orderStatus == null)
        {
            return NotFound("Order status not found");
        }

        if (updatedOrderStatus == null || string.IsNullOrEmpty(updatedOrderStatus.Name))
        {
            return BadRequest("Invalid input");
        }

        orderStatus.Name = updatedOrderStatus.Name;

        _orderStatusRepository.Update(orderStatus);

        return Ok(orderStatus);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var orderStatus = await _orderStatusRepository.GetByIdAsync(id);
        if (orderStatus == null)
        {
            return NotFound("Order status not found");
        }
        _orderStatusRepository.Delete(orderStatus);

        return Ok("Order status deleted");
    }

}
