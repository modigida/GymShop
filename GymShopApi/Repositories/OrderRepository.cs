using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class OrderRepository(AppDbContext context) : GenericRepository<Order>(context)
{

    public override void Update(Order entity)
    {
        if (entity.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be updated after it has been completed.");
        }

        base.Update(entity);
    }

    public override void Delete(Order entity)
    {
        if (entity.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be deleted after it has been completed.");
        }

        base.Delete(entity);
    }
}

