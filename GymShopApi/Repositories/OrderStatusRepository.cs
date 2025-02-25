using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class OrderStatusRepository(AppDbContext context) : GenericRepository<OrderStatus>(context)
{
}
