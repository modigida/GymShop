using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymShopApi.Services;
public class OrderProductService(IUnitOfWork unitOfWork) : GenericService<OrderProduct>(unitOfWork)
{
    public override async Task<OrderProduct> AddAsync(OrderProduct entity)
    {
        if (entity.OrderId <= 0 || entity.ProductId <= 0 || entity.Quantity <= 0 || entity.CurrentPrice <= 0.0)
        {
            throw new ArgumentException("Invalid input.");
        }

        await _repository.AddAsync(entity);
        await unitOfWork.CompleteAsync();

        entity.Order = await unitOfWork.Orders.GetByIdAsync(entity.OrderId);
        entity.Product = await unitOfWork.Products.GetByIdAsync(entity.ProductId);
        return entity;
    }
    public override async Task<OrderProduct> Update(OrderProduct entity, params object[] keyValues)
    {
        var existingOrderProduct = await GetByIdAsync(keyValues);
        if (existingOrderProduct == null)
        {
            throw new ArgumentException("Order product not found.");
        }

        if (entity.Quantity > 0) existingOrderProduct.Quantity = entity.Quantity;
        if (entity.CurrentPrice > 0) existingOrderProduct.CurrentPrice = entity.CurrentPrice;

        await _repository.Update(existingOrderProduct);
        await unitOfWork.CompleteAsync();
        return existingOrderProduct;
    }
    public override async Task Delete(params object[] keyValues)
    {
        var entity = await GetByIdAsync(keyValues);
        if (entity == null)
        {
            throw new ArgumentException("OrderProduct not found.");
        }

        await _repository.Delete(entity);
        await unitOfWork.CompleteAsync();
    }
}
