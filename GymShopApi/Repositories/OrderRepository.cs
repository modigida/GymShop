using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class OrderRepository(AppDbContext context) : GenericRepository<Order>(context)
{

    //  public void Update(Order entity)
    //  {
    //      if (entity.Status == "Delivered")
    //      {
    //          throw new InvalidOperationException("Order cannot be updated after it has been delivered.");
    //      }
    //
    //      _dbSet.Update(entity);
    //  }
    //
    //  public void Delete(Order entity)
    //  {
    //      if (entity.Status == "Delivered")
    //      {
    //          throw new InvalidOperationException("Order cannot be deleted after it has been delivered.");
    //      }
    //
    //      _dbSet.Remove(entity);
    //  }
}

