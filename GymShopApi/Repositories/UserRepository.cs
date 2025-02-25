using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class UserRepository(AppDbContext context) : GenericRepository<User>(context)
{
}
