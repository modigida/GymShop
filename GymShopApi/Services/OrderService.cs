using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class OrderService(IUnitOfWork unitOfWork) : GenericService<Order>(unitOfWork)
{
    public override async Task<Order> AddAsync(Order entity)
    {
        if (entity.UserId == Guid.Empty || entity.PurchaseDate == DateTime.MinValue || entity.OrderStatusId <= 0)
        {
            throw new ArgumentException("Invalid input.");
        }

        await _unitOfWork.Orders.AddAsync(entity);
        await _unitOfWork.CompleteAsync();

        entity.User = await _unitOfWork.Users.GetByIdAsync(entity.UserId);
        entity.OrderStatus = await _unitOfWork.OrderStatuses.GetByIdAsync(entity.OrderStatusId);
        return entity;
    }
    public override async Task<Order> Update(Order entity, params object[] keyValues)
    {
        var order = await GetByIdAsync(keyValues);
        if (order == null)
        {
            throw new ArgumentException("Order not found.");
        }
        if (entity == null)
        {
            throw new ArgumentException("Invalid input.");
        }
        if (entity.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be updated after it has been completed.");
        }

        if (entity.OrderStatusId != 0 && entity.OrderStatusId != order.OrderStatusId)
        {
            order.OrderStatusId = entity.OrderStatusId;
            order.OrderStatus = await _unitOfWork.OrderStatuses.GetByIdAsync(entity.OrderStatusId);
        }

        await _unitOfWork.Orders.Update(order);
        await _unitOfWork.CompleteAsync();
        return order;
    }

    public override async Task Delete(params object[] keyValues)
    {
        var entity = await GetByIdAsync(keyValues);
        if (entity == null)
        {
            throw new ArgumentException("Order not found.");
        }
        if (entity.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be deleted after it has been completed.");
        }
        await _unitOfWork.Orders.Delete(entity);
        await _unitOfWork.CompleteAsync();
    }
}
