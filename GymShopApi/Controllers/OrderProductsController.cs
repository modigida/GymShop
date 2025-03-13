using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderProductsController(IGenericService<OrderProduct> orderProductService) : ControllerBase
{
    //TODO move into OrdersController, only able to get all orderproducts based on productId, delete??

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orderProducts = await orderProductService.GetAllAsync();
        if (!orderProducts.Any())
        {
            return NotFound("No order products found");
        }

        return Ok(orderProducts);
    }

    [HttpGet("{orderId}/{productId}")]
    public async Task<IActionResult> Get(int orderId, int productId)
    {
        var orderProduct = await orderProductService.GetByIdAsync(orderId, productId);
        if (orderProduct == null)
        {
            return NotFound($"No order product found");
        }

        return Ok(orderProduct);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderProduct orderProduct)
    {
        try
        {
            var newOrderProduct = await orderProductService.AddAsync(orderProduct);
            return CreatedAtAction(nameof(Get), new { orderId = newOrderProduct.OrderId, 
                productId = newOrderProduct.ProductId }, newOrderProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{orderId}/{productId}")]
    public async Task<IActionResult> Put(int orderId, int productId, [FromBody] OrderProduct updatedOrderProduct)
    {
        try
        {
            var orderProduct = await orderProductService.Update(updatedOrderProduct, orderId, productId);
            return Ok(orderProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Order product not found");
        }
    }

    [HttpDelete("{orderId}/{productId}")]
    public async Task<IActionResult> Delete(int orderId, int productId)
    {
        try
        {
            await orderProductService.Delete(orderId, productId);
            return Ok("Order product deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Order product not found");
        }
    }
}
