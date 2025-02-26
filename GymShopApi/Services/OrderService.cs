using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class OrderService(IUnitOfWork unitOfWork) : GenericService<Order>(unitOfWork)
{
    public override Task<Order> Update(int id, Order entity)
    {
        if (entity.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be updated after it has been completed.");
        }
        return base.Update(id, entity);
    }

    public override Task Delete(Order entity)
    {
        if (entity.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be deleted after it has been completed.");
        }
        return base.Delete(entity);
    }
}
