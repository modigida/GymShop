using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context)
{
}
