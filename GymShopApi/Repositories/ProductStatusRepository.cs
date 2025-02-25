using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class ProductStatusRepository(AppDbContext context) : GenericRepository<ProductStatus>(context)
{
}
