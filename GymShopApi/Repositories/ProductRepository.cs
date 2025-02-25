using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context)
{
}
