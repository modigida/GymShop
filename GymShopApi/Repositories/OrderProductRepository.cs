using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class OrderProductRepository(AppDbContext context) : GenericRepository<OrderProduct>(context)
{
}
