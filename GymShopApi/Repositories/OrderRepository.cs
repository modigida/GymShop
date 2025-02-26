using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class OrderRepository(AppDbContext context) : GenericRepository<Order>(context)
{

    
}

