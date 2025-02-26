using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderStatusesController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orderStatuses = await _unitOfWork.OrderStatuses.GetAllAsync();

        if (!orderStatuses.Any())
        {
            return NotFound("No order statuses found");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(orderStatuses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var orderStatus = await _unitOfWork.OrderStatuses.GetByIdAsync(id);

        if (orderStatus == null)
        {
            return NotFound($"No order status found with ID: {id}");
        }
        await _unitOfWork.CompleteAsync();
        return Ok(orderStatus);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderStatus orderStatus)
    {
        if (orderStatus == null || string.IsNullOrEmpty(orderStatus.Name))
        {
            return BadRequest("Invalid input");
        }

        await _unitOfWork.OrderStatuses.AddAsync(orderStatus);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(Get), new { id = orderStatus.Id }, orderStatus);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrderStatus updatedOrderStatus)
    {
        var orderStatus = await _unitOfWork.OrderStatuses.GetByIdAsync(id);
        if (orderStatus == null)
        {
            return NotFound("Order status not found");
        }

        if (updatedOrderStatus == null || string.IsNullOrEmpty(updatedOrderStatus.Name))
        {
            return BadRequest("Invalid input");
        }

        orderStatus.Name = updatedOrderStatus.Name;

        _unitOfWork.OrderStatuses.Update(orderStatus);
        await _unitOfWork.CompleteAsync();
        return Ok(orderStatus);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var orderStatus = await _unitOfWork.OrderStatuses.GetByIdAsync(id);
        if (orderStatus == null)
        {
            return NotFound("Order status not found");
        }
        _unitOfWork.OrderStatuses.Delete(orderStatus);
        await _unitOfWork.CompleteAsync();
        return Ok("Order status deleted");
    }

}
