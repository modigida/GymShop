﻿using GymShopApi.Entities;
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
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orderStatuses = await orderStatusService.GetAllAsync();

        if (!orderStatuses.Any())
        {
            return NotFound("No order statuses found");
        }

        return Ok(orderStatuses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var orderStatus = await orderStatusService.GetByIdAsync(id);

        if (orderStatus == null)
        {
            return NotFound($"No order status found with ID: {id}");
        }

        return Ok(orderStatus);
    }

    //[HttpPost]
    //public async Task<IActionResult> Post([FromBody] OrderStatus orderStatus)
    //{
    //    try
    //    {
    //        var newOrderStatus = await orderStatusService.AddAsync(orderStatus);
    //        return CreatedAtAction(nameof(Get), new { id = newOrderStatus.Id }, newOrderStatus);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> Put(int id, [FromBody] OrderStatus updatedOrderStatus)
    //{
    //    try
    //    {
    //        var orderStatus = await orderStatusService.Update(updatedOrderStatus, id);
    //        return Ok(orderStatus);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound("Order status not found");
    //    }
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    try
    //    {
    //        await orderStatusService.Delete(id);
    //        return Ok("Order status deleted");
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound("Order status not found");
    //    }
    //}
}
