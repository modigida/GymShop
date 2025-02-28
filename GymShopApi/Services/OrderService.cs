using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class OrderService(IUnitOfWork unitOfWork) : GenericService<Order>(unitOfWork)
{
    public override Task<Order> AddAsync(Order entity)
    {
        //TODO
        throw new NotImplementedException();
    }
    public override async Task<Order> Update(int id, Order entity)
    {
        var order = await GetByIdAsync(id);
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

    public override async Task Delete(int id)
    {
        var entity = await GetByIdAsync(id);
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
