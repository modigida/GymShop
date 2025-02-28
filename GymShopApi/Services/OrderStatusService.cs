using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class OrderStatusService(IUnitOfWork unitOfWork) : GenericService<OrderStatus>(unitOfWork)
{
    public async Task<bool> NameExistsAsync(string name)
    {
        var orderStatuses = await _unitOfWork.OrderStatuses.GetAllAsync();
        return orderStatuses.Any(c => c.Name == name);
    }
    public override async Task<OrderStatus> AddAsync(OrderStatus entity)
    {
        if (entity == null || string.IsNullOrEmpty(entity.Name))
        {
            throw new ArgumentException("Order status name is required.");
        }
        if (await NameExistsAsync(entity.Name))
        {
            throw new ArgumentException("Order status with the same name already exists.");
        }

        await _unitOfWork.OrderStatuses.AddAsync(entity);
        await _unitOfWork.CompleteAsync();

        return entity;
    }
    public override async Task<OrderStatus> Update(int id, OrderStatus entity)
    {
        var orderStatus = await GetByIdAsync(id);
        if (orderStatus == null)
        {
            throw new ArgumentException("Order status not found.");
        }
        if (entity == null || string.IsNullOrEmpty(entity.Name))
        {
            throw new ArgumentException("Order status name is required.");
        }
        if (await NameExistsAsync(entity.Name))
        {
            throw new ArgumentException("Order status with the same name already exists.");
        }

        orderStatus.Name = entity.Name;

        await _unitOfWork.OrderStatuses.Update(orderStatus);
        await _unitOfWork.CompleteAsync();

        return orderStatus;
    }
}

