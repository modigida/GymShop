using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymShopApi.Controllers;
public class OrderProductsController(IUnitOfWork unitOfWork, IGenericRepository<OrderProduct> orderProductRepository) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<OrderProduct> _orderProductRepository = orderProductRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orderProducts = await _orderProductRepository.GetAllAsync();
        if (!orderProducts.Any())
        {
            return NotFound("No order products found");
        }
        return Ok(orderProducts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var orderProduct = await _orderProductRepository.GetByIdAsync(id);
        if (orderProduct == null)
        {
            return NotFound($"No order product found with ID: {id}");
        }
        return Ok(orderProduct);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderProduct orderProduct)
    {
        if (orderProduct == null || orderProduct.OrderId == 0 || orderProduct.ProductId == 0 || orderProduct.Quantity == 0)
        {
            return BadRequest("Invalid input");
        }
        await _orderProductRepository.AddAsync(orderProduct);
        return CreatedAtAction(nameof(GetAll), new { orderId = orderProduct.OrderId, productId = orderProduct.ProductId }, orderProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrderProduct updatedOrderProduct)
    {
        var orderProduct = await _orderProductRepository.GetByIdAsync(id);
        if (orderProduct == null)
        {
            return NotFound("Order product not found");
        }

        if (updatedOrderProduct.OrderId != 0) { orderProduct.OrderId = updatedOrderProduct.OrderId; }
        if (updatedOrderProduct.ProductId != 0) { orderProduct.ProductId = updatedOrderProduct.ProductId; }
        if (updatedOrderProduct.Quantity != 0) { orderProduct.Quantity = updatedOrderProduct.Quantity; }
        if (updatedOrderProduct.TotalPrice != 0) { orderProduct.TotalPrice = updatedOrderProduct.TotalPrice; }

        _orderProductRepository.Update(updatedOrderProduct);
        return Ok(updatedOrderProduct);
    }

    [HttpDelete("{id")]
    public async Task<IActionResult> Delete(int id)
    {
        var orderProduct = await _orderProductRepository.GetByIdAsync(id);
        if (orderProduct == null)
        {
            return NotFound("Order product not found");
        }
        _orderProductRepository.Delete(orderProduct);
        return Ok("Order product deleted");
    }
}
